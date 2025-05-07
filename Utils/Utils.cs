using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MindFree.Utils;

public static class Utils{
    public static async Task SetFocus(InputText input_name){
		if(input_name.Element.HasValue){
			await input_name.Element.Value.FocusAsync();
		}
	}
}