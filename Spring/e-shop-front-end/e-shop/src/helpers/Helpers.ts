export class HelperMethods {
    public static setAmountOfProductInRow(element: HTMLElement) {
        document.documentElement.style.setProperty('--card-width', `${(element.offsetWidth - 10 * (Number((element.offsetWidth / 490).toPrecision(1)) * 2)) / Number((element.offsetWidth / 490).toPrecision(1))}px`); //change / * 3 to / * 2 for 2 cards per row
        document.documentElement.style.setProperty('--main-width', `${element.offsetWidth}px`);
    }
}
