namespace Aspire.Application.AppServices.Dtos
{
    public class UpdateDto : UpdateDto<long>
    {
    }

    public class UpdateDto<TId>
    {
        public TId Id { get; set; }
    }
}
