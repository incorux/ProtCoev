using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProteinCoev
{
    class Psicov
    {
        private char[,] aln;
        [ThreadStatic]
        static int a, b, i, j, k, ii;

        [ThreadStatic]
        private static double aa, bb, c, sum, delta, dlx;
        public Psicov(char[,] Aln)
        {
            aln = Aln;
            GetPsicov(aln);
        }

        private const double EPS = 1.1e-15;
        private const double BIG = 1e9;
        int glassofast(int n, double[,] S, double[,] L, double thr, int maxit, bool approxflg, bool warm, double[,] X, double[,] W)
        {
            // i,j,ii,wxj,aa,bb,c,dlx,delta,sum
            int iter, jj;
            double dw, shr, thrlasso, tmp;
            var wd = new double[n];
            var wxj = new ThreadLocal<double[]>(() => new double[n]);
            var aLock = new Object();

            for (shr = ii = 0; ii < n; ii++)
                for (jj = 0; jj < n; jj++)
                    shr += Math.Abs(S[ii, jj]);

            for (i = 0; i < n; i++)
                shr -= Math.Abs(S[i, i]);

            if (shr == 0.0)
            {
                /* S is diagonal. */
                for (ii = 0; ii < n; ii++)
                    for (jj = 0; jj < n; jj++)
                        W[ii, jj] = X[ii, jj] = 0.0;

                for (i = 0; i < n; i++)
                    W[i, i] = W[i, i] + L[i, i];

                for (ii = 0; ii < n; ii++)
                    for (jj = 0; jj < n; jj++)
                        X[ii, jj] = 0.0;

                for (i = 0; i < n; i++)
                    X[i, i] = 1.0 / Math.Max(W[i, i], EPS);

                return 0;
            }

            shr *= thr / (n - 1);
            thrlasso = shr / n;
            if (thrlasso < 2 * EPS)
                thrlasso = 2 * EPS;

            if (!warm)
            {
                for (ii = 0; ii < n; ii++)
                    for (jj = 0; jj < n; jj++)
                    {
                        W[ii, jj] = S[ii, jj];
                        X[ii, jj] = 0.0;
                    }
            }
            else
            {
                for (i = 0; i < n; i++)
                {
                    for (ii = 0; ii < n; ii++)
                        X[i, ii] = -X[i, ii] / X[i, i];
                    X[i, i] = 0.0;
                }
            }

            for (i = 0; i < n; i++)
            {
                wd[i] = S[i, i] + L[i, i];
                W[i, i] = wd[i];
            }

            for (iter = 1; iter <= maxit; iter++)
            {
                dw = 0.0;

                //#pragma omp parallel for default(shared) private(i,j,ii,wxj,aa,bb,c,dlx,delta,sum)
                Parallel.For(0, n, j =>
                {
                    for (ii = 0; ii < n; ii++)
                        wxj.Value[ii] = 0.0;

                    for (i = 0; i < n; i++)
                        if (X[j, i] != 0.0)
                            for (ii = 0; ii < n; ii++)
                                wxj.Value[ii] += W[i, ii] * X[j, i];

                    for (; ; )
                    {
                        dlx = 0.0;

                        for (i = 0; i < n; i++)
                        {
                            if (i == j || !(L[j, i] < BIG)) continue;
                            aa = S[j, i] - wxj.Value[i] + wd[i] * X[j, i];
                            bb = Math.Abs(aa) - L[j, i];
                            if (bb <= 0.0)
                                c = 0.0;
                            else if (aa >= 0.0)
                                c = bb / wd[i];
                            else
                                c = -bb / wd[i];

                            delta = c - X[j, i];
                            if (delta == 0.0 || (approxflg && !(Math.Abs(delta) > 1e-6))) continue;
                            X[j, i] = c;

                            for (ii = 0; ii < n; ii++)
                                wxj.Value[ii] += W[i, ii] * delta;

                            if (Math.Abs(delta) > dlx)
                                dlx = Math.Abs(delta);
                        }

                        if (dlx < thrlasso)
                            break;
                    }

                    wxj.Value[j] = wd[j];

                    for (sum = ii = 0; ii < n; ii++)
                        sum += Math.Abs(wxj.Value[ii] - W[j, ii]);

                    //#pragma omp critical
                    lock (aLock)
                        if (sum > dw)
                            dw = sum;

                    for (ii = 0; ii < n; ii++)
                        W[j, ii] = wxj.Value[ii];
                    for (ii = 0; ii < n; ii++)
                        W[ii, j] = wxj.Value[ii];
                });
                if (dw <= shr)
                    break;
            }

            for (i = 0; i < n; i++)
            {
                for (sum = ii = 0; ii < n; ii++)
                    sum += X[i, ii] * W[i, ii];

                tmp = 1.0 / (wd[i] - sum);

                for (ii = 0; ii < n; ii++)
                    X[i, ii] = -tmp * X[i, ii];
                X[i, i] = tmp;
            }

            for (i = 0; i < n - 1; i++)
            {
                for (ii = i + 1; ii < n; ii++)
                {
                    X[i, ii] = 0.5 * (X[i, ii] + X[ii, i]);
                    X[ii, i] = X[i, ii];
                }
            }

            return iter;
        }
        /* Perform Cholesky decomposition on matrix */
        bool test_cholesky(double[,] a, int n)
        {
            int i, j, k;
            var status = false;
            double sum;
            var diag = new double[n];

            for (i = 0; i < n; i++)
            {
                if (status) continue;
                for (j = i; j < n; j++)
                {
                    sum = a[i, j];

                    for (k = i - 1; k >= 0; k--)
                        sum -= a[i, k] * a[j, k];

                    if (i == j)
                    {
                        if (sum <= 0.0)
                            status = true;

                        diag[i] = Math.Sqrt(sum);
                    }
                    else
                        a[j, i] = sum / diag[i];
                }
            }

            return status;
        }

        private int GetPsicov(char[,] aln)
        {
            ///////////////////    Variables  ///////////////////////////////
            bool filtflg = false,
                approxflg = true,
                shrinkflg = true,
                initflg = false,
                apcflg = true,
                rawscflg = true,
                overrideflg = false;
            int nseqs = aln.GetLength(0);
            int seqlen = aln.GetLength(1);
            int s, ncon, opt, ndim, maxit = 10000, npair, nnzero, niter, jerr, pseudoc = 1, minseqsep = 5;
            var ccount = new int[seqlen];
            double thresh = 1e-4, del, pcmean, pc, trialrho, rhodefault = 0.001;
            double[,] pcmat;
            double[] pcsum;
            double sum, score, wtsum, lambda, smean, fnzero, lastfnzero, rfact, r2, targfnzero = 0.1, scsum, scsumsq, mean, sd, zscore, ppv;
            double[,] pa;
            double idthresh = 1.0 - 62 / 100.0, maxgapf = 0.9;
            var buf = new char[4096];
            char[] blockfn = null;
            var seq = new char[seqlen];
            var aLock = new object();
            //weight = allocvec(nseqs, sizeof(double));
            var weight = new double[nseqs];
            //wtcount = allocvec(nseqs, sizeof(unsigned int));
            var wtcount = new uint[nseqs];
            ///////////////////    Variables /////////////////////////////


            /* Calculate sequence weights (use openMP/pthreads if available) */
            if (idthresh < 0.0)
            {
                var meanfracid = 0.0;

                //#pragma omp parallel for default(shared) private(j,k) reduction(+:meanfracid)
                for (i = 0; i < nseqs; i++)
                    for (j = i + 1; j < nseqs; j++)
                    {
                        int nids;
                        double fracid;

                        for (nids = k = 0; k < seqlen; k++)
                            nids += (aln[i, k] == aln[j, k]) ? 1 : 0;

                        fracid = (double)nids / seqlen;

                        meanfracid += fracid;
                    }

                meanfracid /= 0.5 * nseqs * (nseqs - 1.0);

                idthresh = Math.Min(0.6, 0.38 * 0.32 / meanfracid);

                //	printf("idthresh = %f  meanfracid = %f\n", idthresh, meanfracid);
            }

            //#pragma omp parallel for default(shared) private(j,k)

            Parallel.For(0, nseqs, it =>
            {
                for (j = it + 1; j < nseqs; j++)
                {
                    var nthresh = (int)(seqlen * idthresh);


                    for (k = 0; nthresh > 0 && k < seqlen; k++)
                    {
                        nthresh -= (aln[it, k] != aln[j, k]) ? 1 : 0;
                    }
                    if (nthresh <= 0) continue;
                    //#pragma omp criical
                    lock (aLock)
                    {
                        wtcount[it]++;
                        wtcount[j]++;
                    }
                }
            });

            for (wtsum = i = 0; i < nseqs; i++)
                wtsum += (weight[i] = 1.0 / (1 + wtcount[i]));

            //    printf("wtsum = %f\n", wtsum);

            if (wtsum < seqlen && !overrideflg)
                MessageBox.Show("Sorry - not enough sequences or sequence diversity to proceed!");            // wtsum, seqlen


            pa = new double[seqlen, 21];
            /* Calculate singlet frequencies with pseudocount */
            for (i = 0; i < seqlen; i++)
            {
                for (a = 0; a < 21; a++)
                    pa[i, a] = pseudoc;

                for (k = 0; k < nseqs; k++)
                {
                    a = aln[k, i].ToInt();
                    if (a < 21)
                        pa[i, a] += weight[k];
                }

                for (a = 0; a < 21; a++)
                    pa[i, a] /= pseudoc * 21.0 + wtsum;
            }


            ndim = seqlen * 21;
            var rho = new double[ndim, ndim];
            var ww = new double[ndim, ndim];
            var wwi = new double[ndim, ndim];
            var cmat = new double[ndim, ndim];
            var tempmat = new double[ndim, ndim];
            /* Form the covariance matrix */
            //#pragma omp parallel for default(shared) private(j,k,a,b)
            Parallel.For(0, seqlen, it =>
            {
                for (j = it; j < seqlen; j++)
                {
                    var pab = new double[21, 21];

                    for (a = 0; a < 21; a++)
                        for (b = 0; b < 21; b++)
                            if (it == j)
                                pab[a, b] = (a == b) ? pa[it, a] : 0.0;
                            else
                                pab[a, b] = pseudoc / 21.0;

                    if (it != j)
                    {
                        for (k = 0; k < nseqs; k++)
                        {
                            a = aln[k, it].ToInt();
                            b = aln[k, j].ToInt();
                            if (a < 21 && b < 21)
                                pab[a, b] += weight[k];
                        }

                        for (a = 0; a < 21; a++)
                            for (b = 0; b < 21; b++)
                                pab[a, b] /= pseudoc * 21.0 + wtsum;
                    }

                    for (a = 0; a < 21; a++)
                        for (b = 0; b < 21; b++)
                            if (it != j || a == b)
                                cmat[it * 21 + a, j * 21 + b] = cmat[j * 21 + b, it * 21 + a] = pab[a, b] - pa[it, a] * pa[j, b];
                }
            });
            /* Shrink sample covariance matrix towards shrinkage target F = Diag(1,1,1,...,1) * smean */

            if (shrinkflg)
            {
                for (smean = i = 0; i < ndim; i++)
                    smean += cmat[i, i];

                smean /= ndim;
                lambda = 0.2;

                for (; ; )
                {
                    Buffer.BlockCopy(cmat, 0, tempmat, 0, cmat.Length * sizeof(double));

                    /* Test if positive definite using Cholesky decomposition */
                    if (!test_cholesky(tempmat, ndim))
                        break;

                    //#pragma omp parallel for default(shared) private(j,a,b)
                    Parallel.For(0, seqlen, i =>
                    {
                        {
                            for (j = 0; j < seqlen; j++)
                                for (a = 0; a < 21; a++)
                                    for (b = 0; b < 21; b++)
                                        if (i != j)
                                            cmat[i * 21 + a, j * 21 + b] *= 1.0 - lambda;
                                        else if (a == b)
                                            cmat[i * 21 + a, j * 21 + b] = smean * lambda + (1.0 - lambda) * cmat[i * 21 + a, j * 21 + b];
                        }
                    });
                }
            }

            lastfnzero = 0.0;

            /* Guess at a reasonable starting rho value if undefined */
            trialrho = rhodefault < 0.0 ? Math.Max(0.001, 1.0 / wtsum) : rhodefault;

            rfact = 0.0;

            for (; ; )
            {
                double targdiff, besttd = BIG, bestrho = 0;

                if (trialrho <= 0.0 || trialrho >= 1.0)
                {
                    /* Give up search - recalculate with best rho found so far and exit */
                    trialrho = bestrho;
                    targfnzero = 0.0;
                }

                for (i = 0; i < ndim; i++)
                    for (j = 0; j < ndim; j++)
                        rho[i, j] = trialrho;

                for (i = 0; i < seqlen; i++)
                    for (j = 0; j < seqlen; j++)
                        for (a = 0; a < 21; a++)
                            for (b = 0; b < 21; b++)
                                if ((a != b && i == j) || pa[i, 20] > maxgapf || pa[j, 20] > maxgapf)
                                    rho[i * 21 + a, j * 21 + b] = BIG;

                glassofast(ndim, cmat, rho, thresh, maxit, approxflg, initflg, wwi, ww);
                /* Don't attempt interation if too few sequences */
                if (targfnzero <= 0.0 || wtsum < seqlen)
                    break;

                for (npair = nnzero = i = 0; i < ndim; i++)
                    for (j = i + 1; j < ndim; j++, npair++)
                        if (wwi[i, j] != 0.0)
                            nnzero++;

                fnzero = (double)nnzero / npair;

                //	printf("rho=%f fnzero = %f\n", trialrho, fnzero);

                /* Stop iterating if we have achieved the target sparsity level */

                targdiff = Math.Abs(fnzero - targfnzero) / targfnzero;

                if (targdiff < 0.01)
                    break;

                if (targdiff < besttd)
                {
                    besttd = targdiff;
                    bestrho = trialrho;
                }

                if (fnzero == 0.0)
                {
                    /* As we have guessed far too high, halve rho and try again */
                    trialrho *= 0.5;
                    continue;
                }

                if (lastfnzero > 0.0 && fnzero != lastfnzero)
                {
                    //	    printf("fnzero=%f lastfnzero=%f trialrho=%f oldtrialrho=%f\n", fnzero, lastfnzero, trialrho, trialrho/rfact);

                    rfact = Math.Pow(rfact, Math.Log(targfnzero / fnzero) / Math.Log(fnzero / lastfnzero));

                    //	    printf("New rfact = %f\n", rfact);
                }

                lastfnzero = fnzero;

                /* Make a small trial step in the appropriate direction */

                if (rfact == 0.0)
                    rfact = (fnzero < targfnzero) ? 0.9 : 1.1;

                trialrho *= rfact;
            }

            /* Calculate background corrected scores using average product correction */

            pcmat = new double[seqlen, seqlen];
            pcsum = new double[seqlen];
            pcmean = 0.0;

            for (i = 0; i < seqlen; i++)
                for (j = i + 1; j < seqlen; j++)
                {
                    for (pc = a = 0; a < 20; a++)
                        for (b = 0; b < 20; b++)
                            pc += Math.Abs(wwi[i * 21 + a, j * 21 + b]);

                    pcmat[i, j] = pcmat[j, i] = pc;
                    pcsum[i] += pc;
                    pcsum[j] += pc;

                    pcmean += pc;
                }

            pcmean /= seqlen * (seqlen - 1) * 0.5;

            /* Build final list of predicted contacts */

            var sclist = new sc_entry[seqlen * (seqlen - 1) / 2];

            for (scsum = scsumsq = ncon = i = 0; i < seqlen; i++)
                for (j = i + minseqsep; j < seqlen; j++)
                    if (pcmat[i, j] > 0.0)
                    {
                        /* Calculate APC score */
                        if (apcflg)
                            sclist[ncon].sc = pcmat[i, j] - pcsum[i] * pcsum[j] / SQR(seqlen - 1.0) / pcmean;
                        else
                            sclist[ncon].sc = pcmat[i, j];
                        scsum += sclist[ncon].sc;
                        scsumsq += SQR(sclist[ncon].sc);
                        sclist[ncon].i = i;
                        sclist[ncon++].j = j;
                    }

            mean = scsum / ncon;
            sd = 1.25 * Math.Sqrt(scsumsq / ncon - SQR(mean)); /* Corrected for extreme-value bias */

            for (i = 0; i < seqlen; i++)
                ccount[i] = 0;

            Array.Sort(sclist, new scComparer());

            /* Print output in CASP RR format with optional PPV estimated from final Z-score */
            if (!rawscflg)
                for (i = 0; i < ncon; i++)
                    Console.WriteLine("{0} {1} 0 8 {2}\n", sclist[i].i + 1, sclist[i].j + 1, sclist[i].sc);
            else
                for (i = 0; i < ncon; i++)
                {
                    zscore = (sclist[i].sc - mean) / sd;
                    ppv = 0.904 / (1.0 + 16.61 * Math.Exp(-0.8105 * zscore));
                    if (!(ppv >= 0.5) && (ccount[sclist[i].i] != 0 && ccount[sclist[i].j] != 0) && filtflg) continue;
                    Console.WriteLine("{0} {1} 0 8 {2}\n", sclist[i].i + 1, sclist[i].j + 1, ppv);
                    ccount[sclist[i].i]++;
                    ccount[sclist[i].j]++;
                }
            return 0;
        }

        private class scComparer : IComparer<sc_entry>
        {
            public int Compare(sc_entry x, sc_entry y)
            {
                return x.sc.CompareTo(y.sc);
            }
        }

        private double SQR(double x)
        {
            return x * x;
        }
        public struct sc_entry
        {
            public double sc;
            public int i, j;
        }
    }
}
