using X.PagedList;

namespace BioStore.Admin.Queries
{
    public class GradeResult
    {
        public int? pageSize;
        public StaticPagedList<ListaDeGradeResult> Grades { get; set; }
    }
}
