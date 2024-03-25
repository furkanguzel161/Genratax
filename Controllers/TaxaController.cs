
using Genratax.Models;
using Genratax.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata.Ecma335;

namespace Genratax.Controllers
{
    public class TaxaController:Controller
    {
        private readonly DataContext _context;
        public TaxaController(DataContext context){
            _context = context;
        }
        public IActionResult Index(){
            return RedirectToAction("Random");
        }
        public string Random(){
                  
                     
         //kodu acynron yap
        string randox(int arg){
            Random rnd = new Random();     
            var randomtax = arg;
            var taxs = _context.Taxas.Where(d => d.parent_taxid.Equals(randomtax)).Select(e => e.ncbi_taxid).ToArray();
            var Randomtax = taxs[rnd.Next(taxs.Length)];
            while(true){
                taxs = _context.Taxas.Where(d => d.parent_taxid.Equals(randomtax)).Select(e => e.ncbi_taxid).ToArray();
                
                if (taxs.Length !=0){
                    Randomtax = taxs[rnd.Next(taxs.Length)];
                    randomtax = Randomtax;
                }else{
                    break;
                }
                    
            }return _context.Taxas.Where(d => d.ncbi_taxid.Equals(randomtax)).Select(e => e.tax_name).ToArray()[0];
        }
                
             return randox(32523);
           
            } 
                
    
           
        }
    }
