using System.ComponentModel.DataAnnotations;
using Nordea.Models.Enum;

namespace Nordea.Models.ViewModels
{
    public class IndexViewModel
    {

        [Required]
        [Display(Name = "Text")]
        public string Text { get; set; }
        [Display(Name = "Result Type")]
        public ResultType ResultType { get; set; }
    }
}