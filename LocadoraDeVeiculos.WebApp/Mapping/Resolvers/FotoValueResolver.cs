using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloVeiculo;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping.Resolvers;

public class FotoValueResolver : IValueResolver<FormularioVeiculoViewModel, Veiculo, byte[]>
{
    public FotoValueResolver() { }

    public byte[] Resolve(
        FormularioVeiculoViewModel source,
        Veiculo destination,
        byte[] destMember,
        ResolutionContext context
    )
    {
        using (var memoryStream = new MemoryStream())
        {
            source.Foto.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }
}