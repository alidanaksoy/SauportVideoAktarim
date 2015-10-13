using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoAktarim.Models;

namespace VideoAktarim.Controllers
{
    // Render edilerek LESSON_CONTROL tablouna basılmış videoları alır ve 246 sunucusuna taşımak üzere uygun formatta HDDye taşır.
    public class CopyToHddController : Controller
    {
        SAUPORTDB db = new SAUPORTDB();
        
        public ActionResult Index()
        {
            //CopyHdd();
            return View();
        }
        
        public void CopyHdd()
        {
            var KopyalanacakAnaDizin = @"\\10.9.16.2\246Dosyalar\";
            StreamWriter sw = System.IO.File.AppendText(@"\\10.9.16.2\246Dosyalar\log.txt");

            Guid? mcourseid1 = Guid.Parse("BFCDF59B-9DB6-4D7F-91AD-88D2B9D7B205"); // yok
            Guid? mcourseid2 = Guid.Parse("B4BEEE08-FC59-4D21-9141-88D19BC5BBE5"); // yok

            var kopyalanacaklar = db.WL_LS_LESSON_CONTROL.Where(c => c.STATUS == 3 && (c.MCOURSEID == mcourseid1 || c.MCOURSEID == mcourseid2)).ToList();

            foreach (var item in kopyalanacaklar)
            {
                if (item.MCOURSEID != null && item.ELESSONID != null && item.PATH != null)
                {
                    var KopyalanacakAltDizin = item.MCOURSEID + @"\" + item.ELESSONID + @"\";
                    if (!Directory.Exists(KopyalanacakAnaDizin + KopyalanacakAltDizin))
                    {
                        Directory.CreateDirectory(KopyalanacakAnaDizin + KopyalanacakAltDizin);
                    }
                    if (System.IO.File.Exists(item.PATH)) // Kopyalanacak dosya varsa kopyalıyor
                    {
                        FileInfo fi = new FileInfo(item.PATH);
                        var fileName = fi.Name;

                        System.IO.File.Copy(item.PATH, KopyalanacakAnaDizin + KopyalanacakAltDizin + @"\" + fileName);
                    }
                    else                       // Yok ise log dosyasına basıyor.
                    {
                        sw.WriteLine("MCOURSEID : " + item.MCOURSEID + " ELESSONID : " + item.ELESSONID);
                    }
                }
            }
        }
    }
}