using System.Threading;
using Cysharp.Threading.Tasks;

public class EquipmentCommand : AppCommand
{
	public override string CommandName => "eq";

	public override string CommandDescription => LOC.UI.EquiupmentCommandDescription;

	public override async UniTask Start(BotStatusAppPopup popup, StringUtils.ParsedCommandData input, CancellationToken cancellationToken)
	{
		await popup.ShowEquipment(cancellationToken);
	}
}
