using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oaww.Entity;
using System.Data.SqlClient;

namespace Oaww.Business
{
    public class HomeService : Oaww.BaseBusiness.BaseBusiness
    {
        public List<MessageItem> GetMessageItemByModelID(string ModelID, int top)
        {
            string sql = $@"select top {top}
                            t.ItemID,
                            t.Title,
                            t.ModelID,
                            t.PublicshDate,
                            t.UnPublishDate,
                            isnull(s.Group_Name,'無分類') as GroupName,
                            t.RelateImageFileName,
                            t.LinkUrl,
                            t.Link_Mode
                             from MessageItem t  
                            left join GroupMessage s on t.ModelID = s.Main_ID and t.GroupID = s.ID
                            where t.ModelID =@ModelID and t.IsVerift = 1 and t.Enabled =1
                             and  isnull(t.StDate,'1999/1/1') <= convert(date,GetDate()) 
                             and isnull(t.EdDate,'9999/12/31') >= convert(date,GetDate()) 
                            order by t.Sort";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ModelID", ModelID));

            return base.SearchList<MessageItem>(sql);
        }

        public List<MessageItem> GetMessageItemByModelID(string ModelID, int top,int lang)
        {
            string sql = $@"select top {top}
                            t.ItemID,
                            t.Title,
                            t.ModelID,
                            t.PublicshDate,
                            t.UnPublishDate,
                            isnull(s.Group_Name,'無分類') as GroupName,
                            t.RelateImageFileName,
                            t.LinkUrl,
                            t.Link_Mode,
                            t.Introduction,
                            t.HtmlContent
                             from MessageItem t  
                            left join GroupMessage s on t.ModelID = s.Main_ID and t.GroupID = s.ID
                            where t.ModelID =@ModelID and t.IsVerift = 1 and t.Enabled =1
                             and  isnull(t.StDate,'1999/1/1') <= convert(date,GetDate()) 
                             and isnull(t.EdDate,'9999/12/31') >= convert(date,GetDate()) 
                             and Lang_ID=@Lang_ID
                            order by t.Sort";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ModelID", ModelID));
            base.Parameter.Add(new SqlParameter("@Lang_ID", lang));

            return base.SearchList<MessageItem>(sql);
        }
        public List<MessageItem> GetMessageItemByModelIDWithHome(string ModelID, int top)
        {
            string sql = $@"select top {top}
                            t.ItemID,
                            t.Title,
                            t.ModelID,
                            t.PublicshDate,
                            t.UnPublishDate,
                            isnull(s.Group_Name,'無分類') as GroupName,
                            t.RelateImageFileName,
                            t.LinkUrl,
                            t.Link_Mode
                             from MessageItem t  
                            left join GroupMessage s on t.ModelID = s.Main_ID and t.GroupID = s.ID
                            where t.ModelID =@ModelID and t.IsVerift = 1 and t.Enabled =1
                             and  isnull(t.StDate,'1999/1/1') <= convert(date,GetDate()) 
                             and isnull(t.EdDate,'9999/12/31') >= convert(date,GetDate()) 
                             and Home =1
                            order by t.Sort";

            base.Parameter.Clear();
            base.Parameter.Add(new SqlParameter("@ModelID", ModelID));

            return base.SearchList<MessageItem>(sql);
        }
        //public List<MessageItem> GetGallery()
        //{
        //    string sql = @"select top 3 * from MessageImage t
        //                    where t.UploadFilePath like '\GalleryItem%'
        //                    order by t.FID ";

        //    return base.SearchList<MessageImage>(sql);
        //}
    }
}
