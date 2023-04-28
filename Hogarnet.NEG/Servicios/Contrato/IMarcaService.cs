using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hogarnet.DTO;

namespace Hogarnet.NEG.Servicios.Contrato
{
    public interface IMarcaService
    {
        Task<List<MarcaDTO>> Lista();
    }
}
