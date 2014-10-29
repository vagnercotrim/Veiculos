using System.ComponentModel;

namespace Veiculos.Models
{
    public enum Situacao
    {

        [Description("em uso")]
        Emuso,

        [Description("doado")]
        Doado,

        [Description("leiloado")]
        Leiloado
        
    }
}