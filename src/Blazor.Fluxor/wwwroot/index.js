var TryInitializeFluxor = function () {
	var initializeFluxorScriptElement = document.getElementById("InitializeFluxor");
	if (initializeFluxorScriptElement) {
		let script = initializeFluxorScriptElement.innerHTML || "";
		script = script.replace("<!--!-->", "");
		if (script) {
			eval(script);
		}
		return true;
	}
	return false;
};
