using Cysharp.Threading.Tasks;

public interface IToy
{
	ToySingleton.ToyGenerator GeneratedFrom { get; }

	float Insertion { get; }

	bool CumButtonClicked { get; }

	bool Active { get; }

	string StatusDescription { get; }

	UniTask Connect(IProgressManager frameDisplayProgressManager);

	void Update();

	void Close();

	void CalibrateStart();
}
