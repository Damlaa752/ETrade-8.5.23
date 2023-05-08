using System.ComponentModel.DataAnnotations;

namespace ETrade.Entity.Models.Entities
{
    public enum OrderState
    {
        [Display(Name ="Sipariş Bekleniyor")]
        Waiting,
        [Display(Name = "Sipariş Tamamlandı")]
        Completed
    }
}