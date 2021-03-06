using X.PagedList;

namespace BioStore.Site.Queries
{
    public class GradeResult
    {
        public int? pageSize;
        public StaticPagedList<ListaDeGradeResult> Grades { get; set; }
    }
}
