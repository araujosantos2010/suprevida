using X.PagedList;

namespace BioStore.Site.Queries
{
    public class MarcaResult
    {
        public int? pageSize;
        public StaticPagedList<ListaDeMarcaResult> Marcas { get; set; }
    }
}
