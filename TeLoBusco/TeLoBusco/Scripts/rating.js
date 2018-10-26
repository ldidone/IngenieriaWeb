(function ($) { 
    function Puntuacion() {
        var $this = this;
        function initialize() {
            $(".star").click(function () {
                $(".star").removeClass('active');
                $(this).addClass('active');
                var starValue = $(this).data("value");
                $("#Puntuacion").val(starValue);
            })
        }
        $this.init = function () {
            initialize();
        }
    }
    $(function () {
        var self = new Puntuacion();
        self.init();
    })
}(jQuery))