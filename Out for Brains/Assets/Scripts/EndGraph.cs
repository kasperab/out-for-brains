using UnityEngine;
using Unity.VisualScripting;

public class EndGraph : Unit
{
	[DoNotSerialize]
	public ControlInput inputTrigger;

	[DoNotSerialize]
	public ControlOutput outputTrigger;

	[DoNotSerialize]
	public ValueInput stateMachine;

	protected override void Definition()
	{
		inputTrigger = ControlInput("", (flow) => {
			flow.GetValue<StateMachine>(stateMachine).enabled = false;
			return outputTrigger;
		});
		outputTrigger = ControlOutput("");
		stateMachine = ValueInput<StateMachine>("stateMachine", null);
	}
}
