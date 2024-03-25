using System.ComponentModel.DataAnnotations;


namespace Genratax.Data
{
    public class Taxa{
        [Key]
        public int ncbi_taxid {get;set;}
        public int parent_taxid {get;set;}
        public string tax_name {get;set;} = null!;
        public string lineage_level {get;set;} = null!;
    
    }
}