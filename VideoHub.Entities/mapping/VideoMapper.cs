using System;
using System.Collections.Generic;
using System.Text;

namespace VideoHub.Entities.mapping
{
    public class VideoMapper:Video
    {
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public List<Tag> Tags { get; set; }
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public List<Category> Categories { get; set; }
    }

}
