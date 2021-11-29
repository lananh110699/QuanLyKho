﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace QuanLyKho.Models
{
    public class AutoGenerateKey
    {
        public string GenerateKey(string ID)
        {
            string strKey = "";
            string numPart = "", strPart = "", strPhanSo = "";
            numPart = Regex.Match(ID, @"\d+").Value;
            strPart = Regex.Match(ID, @"\D+").Value;
            int phanso = (Convert.ToInt32(numPart) + 1);
            for (int i = 0; i < numPart.Length - phanso.ToString().Length; i++)
            {
                strPhanSo += "0";
            }
            strPhanSo += phanso;
            //tach phan chu
            strKey = strPart + strPhanSo;
            return strKey;
        }

        internal object GenerateKey(int mPNID)
        {
            throw new NotImplementedException();
        }
    }
}