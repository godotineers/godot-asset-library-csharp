using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Common.Domain
{
    [Table("as_versions")]
    public class GodotVersion
    {
        [Key]
        public int Id { get; set; }
        public string Tag { get; set; }
    }
}
