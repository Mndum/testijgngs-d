using System.Collections.Generic;

public interface IBranchContainer<TBranch>
{
	IEnumerable<TBranch> GetBranches();
}
