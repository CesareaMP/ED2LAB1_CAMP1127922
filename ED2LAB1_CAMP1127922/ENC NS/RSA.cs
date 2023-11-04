using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED2LAB1_CAMP1127922.ENC_NS
{
    public class RSAA
    {
        private long public1;
        private long private1;
        private long common;
            public RSAA(long primo1, long primo2)
            {
            long asd1 = primo1;
            long asd2 = primo2;
            long n = primo1 * primo2;
            long omega = (primo1 - 1) * (primo2 - 1);
            long mcdResult = 2;
            long _e = 2;
            while (mcdResult != 1 || _e % 2 != 0)
            {
                mcdResult = MCD(omega, _e);
                _e = _e + 1;
            }
            long e = _e - 1;

            long mod = 0;
            long j = 0;
            while (mod != 1)
            {
                j = j + 1;
                mod = (e * j) % omega;
            }
            this.public1 = e;
            this.private1 = j;
            this.common = n;            
            }

            public RSAA()
            {

            }

        public (long public1, long private1, long common) obtainKeys ()
        {
            return (public1,private1,common);
        }

        public string Crypt(string msg, long public1, long common)
        {
            string cryptArr = "";
            foreach (char ch in msg)
            {
                long chlong = (int)ch;
                long cyphlong = ExponenciacionBinaria(chlong, public1, common);
                cryptArr+=Convert.ToString(cyphlong % common)+",";
            }
            cryptArr = cryptArr.TrimEnd(',');
            return cryptArr;
        }

        public string Decrypt(string cryptRSA, long private1, long common)
        {
            string msg = "";
            foreach (string vallong in cryptRSA.Split(','))
            {
                long valExp = ExponenciacionBinaria(long.Parse(vallong), private1, common);
                valExp = valExp % common;
                msg += (char)valExp;
            }
            return msg;
        }

        private long ExponenciacionBinaria(long baseNum, long exponente, long common)
        {
            if (exponente == 0)
            {
                return 1;
            }
            long resultado = 1;
            while (exponente > 0)
            {
                if (exponente % 2 == 1)
                {
                    resultado = (resultado * baseNum) % common;
                }
                baseNum = (baseNum * baseNum) % common;
                exponente = exponente / 2;
            }
            return resultado;
        }

        private long MCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}
