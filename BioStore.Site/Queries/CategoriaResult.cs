using X.PagedList;

namespace BioStore.Site.Queries
{
    public class CategoriaResult
    {
        public int? pageSize;
        public StaticPagedList<ListaDeCategoriaResult> Categorias { get; set; }
    }
}
