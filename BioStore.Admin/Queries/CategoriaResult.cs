using X.PagedList;

namespace BioStore.Admin.Queries
{
    public class CategoriaResult
    {
        public int? pageSize;
        public StaticPagedList<ListaDeCategoriaResult> Categorias { get; set; }
    }
}
