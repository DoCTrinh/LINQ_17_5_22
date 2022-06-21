using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LINQ_17_5_22.DTO;

namespace LINQ_17_5_22.BLL
{
    public class BLLQLSV
    {
        private static BLLQLSV instance;
        private delegate int compare(SV a, SV b);
        private compare onedel;
        public static BLLQLSV Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BLLQLSV();
                }
                return instance;
            }
            private set { }
        }


        private BLLQLSV() { }
        public List<CBBItem> GetCBB()
        {
            List<CBBItem> data = new List<CBBItem>();
            using (QLSV_DB db = new QLSV_DB())
            {
                data = (from p in db.lopshes select new CBBItem()
                {
                    value = p.lopsh_id,
                    text = p.lopsh_name,
                }).ToList();
            }
            return data;
        }
        public List<SV>  GetAllSVByIDLop (int lopshid, string search)
        {
            List<SV> data = new List<SV>();
            using (QLSV_DB db = new QLSV_DB())
            {
                if (lopshid == 0) {
                    data = db.svs.Where(p=> p.lopsh.lopsh_name.Contains(search)
                                    || p.hoten.Contains(search) || p.dtb.ToString().Contains(search)
                                    || p.mssv.Contains(search)).Select(p => new SV()
                                    {
                                        mssv = p.mssv,
                                        hoten = p.hoten,
                                        lopsh_name = p.lopsh.lopsh_name,
                                        gender = (bool)p.gender,
                                        ngaysinh = (DateTime)p.ngaysinh,
                                        dtb = (double)p.dtb,
                                        anh = (bool)p.anh,
                                        hocba = (bool)p.hocba,
                                        cmnd = (bool)p.cmnd
                                    }).ToList();
                }
                else
                {
                    data = db.svs.Where(p => p.lopsh_id == lopshid
                                    && (p.lopsh.lopsh_name.Contains(search)
                                    || p.hoten.Contains(search) || p.dtb.ToString().Contains(search)
                                    || p.mssv.Contains(search))).Select(p => new SV()
                                    {
                                        mssv = p.mssv,
                                        hoten = p.hoten,
                                        lopsh_name = p.lopsh.lopsh_name,
                                        gender = (bool)p.gender,
                                        ngaysinh = (DateTime)p.ngaysinh,
                                        dtb = (double)p.dtb,
                                        anh = (bool)p.anh,
                                        hocba = (bool)p.hocba,
                                        cmnd = (bool)p.cmnd
                                    }).ToList();
                }
            }
            return data;
        }
        public SV GetSVByMSSV(string mssv)
        {
            using (QLSV_DB db = new QLSV_DB())
            {
                return db.svs.Where(p => p.mssv == mssv).Select(p => new SV()
                {
                    mssv = p.mssv,
                    hoten = p.hoten,
                    lopsh_name = p.lopsh.lopsh_name,
                    gender = (bool)p.gender,
                    ngaysinh = (DateTime)p.ngaysinh,
                    dtb = (double)p.dtb,
                    anh = (bool)p.anh,
                    hocba = (bool)p.hocba,
                    cmnd = (bool)p.cmnd
                }).FirstOrDefault();
            }
        }
        public bool AddNewSV(sv s)
        {
            using (QLSV_DB db = new QLSV_DB())
            {
                if (db.svs.Find(s.mssv) != null)
                {
                    return false;
                }
                db.svs.Add(s);
                db.SaveChanges();
                return true;
            }
        }
        public void UpdateExistSV(sv s)
        {
            using (QLSV_DB db = new QLSV_DB())
            {
                sv sk = db.svs.Where(p => p.mssv == s.mssv).FirstOrDefault();
                sk.hoten = s.hoten;
                sk.lopsh_id = s.lopsh_id;
                sk.gender = s.gender;
                sk.ngaysinh = s.ngaysinh;
                sk.dtb = s.dtb;
                sk.anh = s.anh;
                sk.hocba = s.anh;
                sk.cmnd = s.cmnd;
                db.SaveChanges();
            }
        }
        public void DeleteSV(string mssv)
        {
            using (QLSV_DB db =new QLSV_DB())
            {
                sv s = db.svs.Find(mssv);
                db.svs.Remove(s);
                db.SaveChanges();
            }
        }
        int compByMSSV(SV sv1, SV sv2)
        {
            return string.Compare(sv1.mssv, sv2.mssv);
        }
        int compByNameSV(SV sv1, SV sv2)
        {
            return string.Compare(sv1.hoten, sv2.hoten);
        }
        int compByDTB(SV sv1, SV sv2)
        {
            return sv1.dtb.CompareTo(sv2.dtb);
        }
        int compByNgaySinh(SV sv1, SV sv2)
        {
            return DateTime.Compare(sv1.ngaysinh, sv2.ngaysinh);
        }
        public List<SV> SortSVWithOption(List<SV> data, int index)
        {
            if (index == 0)
            {
                onedel = new compare(compByMSSV);
            }
            else if (index == 1)
            {
                onedel = new compare(compByNameSV);
            }
            else if (index == 2)
            {
                onedel = new compare(compByNgaySinh);
            }
            else if (index == 3)
            {
                onedel = new compare(compByDTB);
            }
            return SortSV(data, onedel);
        }
        private List<SV> SortSV(List<SV> data, compare comp)
        {
            for (int i = 0; i < data.Count - 1; i++)
            {
                for (int j = i + 1; j < data.Count; j++)
                {
                    if (comp(data[i], data[j]) > 0)
                    {
                        SV sv = data[i];
                        data[i] = data[j];
                        data[j] = sv;
                    }
                }
            }
            return data;
        }
    }
}
