using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using MewToColViewWebApp.Models;

namespace MewToColViewWebApp.Controllers
{
    public class DBAccess
    {

        //
        // Delete(Person p)
        // 削除対象と同じIdを持っているPersonインスタンスを削除する
        //
        public void Delete(int _id)
        {
            using (var ctx = new fp7dataEntities1())
            {
                var deleteTarget = ctx.ex1table.Find(_id);

                try
                {
                    ctx.ex1table.Remove(deleteTarget);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                ctx.SaveChanges();
            }
        }


        public void Edit(int _id, int[] _val)
        {
            using (var ctx = new fp7dataEntities1())
            {
                var editRecord = ctx.ex1table.Find(_id);

                editRecord.val1 = (short)_val[0];
                editRecord.val2 = (short)_val[1];
                editRecord.val3 = (short)_val[2];
                editRecord.val4 = (short)_val[3];
                editRecord.val5 = (short)_val[4];
                editRecord.val6 = (short)_val[5];
                editRecord.val7 = (short)_val[6];
                editRecord.val8 = (short)_val[7];
                editRecord.time = DateTime.Now;
                ctx.SaveChanges();
            }
        }

        public int[] Search(string keyword)
        {
            int[] hit = new int[0];
            int hitcount = 0;

            using (var ctx = new fp7dataEntities1())
            {
                foreach (ex1table searchRecord in ctx.ex1table)
                {
                    // Keyword文字列がIDに含まれるか
                    if (keyword.Length <= searchRecord.id.ToString().Length)
                    {
                        if (isHitIndex(searchRecord.id.ToString(), keyword))
                        {
                            hitcount += 1;
                            Array.Resize(ref hit, hitcount);
                            hit[hitcount - 1] = (int)searchRecord.id;
                            continue;
                        }
                    }
                    

                    // Keyword文字列が年月日に含まれるか
                    if (keyword.Length <= searchRecord.time.ToLongDateString().Length)
                    {
                        if (isHitIndex(searchRecord.time.ToLongDateString(), keyword))
                        {
                            hitcount += 1;
                            Array.Resize(ref hit, hitcount);
                            hit[hitcount - 1] = (int)searchRecord.id;
                            continue;
                        }
                    }

                    // Keyword文字列が時分秒に含まれるか
                    if (keyword.Length < searchRecord.time.ToLongTimeString().Length)
                    {
                        if (isHitIndex(searchRecord.time.ToLongTimeString(), keyword))
                        {
                            hitcount += 1;
                            Array.Resize(ref hit, hitcount);
                            hit[hitcount - 1] = (int)searchRecord.id;
                            continue;
                        }
                    }
                }
            }

            return hit;
        }

        private Boolean isHitIndex(string searchFrom, string searchWord)
        {
            int hitPosition = searchFrom.IndexOf(searchWord);
            if (hitPosition > -1) return true;
            else return false;
        }

        public void ClearAll()
        {

            Database.SetInitializer(new DropCreateDatabaseAlways<fp7dataEntities1>());

            //Database.SetInitializer(new DBAccess());
        }
    }
}