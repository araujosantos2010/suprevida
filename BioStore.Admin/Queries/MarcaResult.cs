using X.PagedList;

namespace BioStore.Admin.Queries
{
    public class MarcaResult
    {
        public int? pageSize;
        public StaticPagedList<ListaDeMarcaResult> Marcas { get; set; }
    }
}
