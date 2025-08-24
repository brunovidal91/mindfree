
// --------------------- Modal ------------------------
function CloseButtons() {

	const closeButtons = document.querySelectorAll(".btn_close");
	closeButtons.forEach(button => {
		button.addEventListener('click', () => {
			const modalId = button.getAttribute('data-modal');
			const modal = document.getElementById(modalId);

			modal.close();
		});
	});
}
function OpenMyModal() {

    const openModalButtons = document.querySelectorAll(".openModal");

    openModalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalId = button.getAttribute('data-modal');
            const modal = document.getElementById(modalId);

            modal.showModal();
        });
    });
}
function SetFocus(id) {
    const inputToFocus = document.getElementById(id);
    inputToFocus && inputToFocus.focus();
}
const previousSelected = [];
function changeLineColor(id) {

    if (previousSelected.length > 0) {
        turnBackLineColor(previousSelected[0]);
        previousSelected.pop();

    }

    previousSelected.push(id);
    //$(`#${id}`).closest("div").css('background-color', '#ff0');
    $(`#${id}`).css('border-width', '2px');
    $(`#${id}`).css('border-color', 'tomato');

}
function turnBackLineColor(id) {
    $(`#${id}`).css('border-color', '#eee');
}