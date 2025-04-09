window.registerExternalNavigationHandler = function () {
    document.addEventListener('click', function (event) {
        const anchor = findClosestAnchor(event.target);
        if (!anchor) return;

        const href = anchor.getAttribute('href');
        if (!href || !href.startsWith('/app/')) {
            return;
        }
    });

    function findClosestAnchor(element) {
        while (element && element !== document.body) {
            if (element.tagName && element.tagName.toLowerCase() === 'a') {
                return element;
            }
            element = element.parentElement;
        }
        return null;
    }
    window.blazorHelpers = {
        navigateTo: function (url) {
            window.location.href = url;
        }
    };
};
