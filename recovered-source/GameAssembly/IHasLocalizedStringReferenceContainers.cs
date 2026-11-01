using System.Collections.Generic;

public interface IHasLocalizedStringReferenceContainers
{
	IEnumerable<LocalizedStringReferenceContainer> GetAllContainers();
}
