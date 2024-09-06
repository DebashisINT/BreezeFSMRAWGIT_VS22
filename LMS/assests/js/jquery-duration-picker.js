(function ($) {

var langs = {
    en: {
        day: 'day',
        hour: 'hour',
        minute: 'minute',
        second: 'second',
        days: 'days',
        hours: 'hours',
        minutes: 'minutes',
        seconds: 'seconds'
    },
    es: {
        day: 'd&iacute;a',
        hour: 'hora',
        minute: 'minuto',
        second: 'segundo',
        days: 'd&iacute;s',
        hours: 'horas',
        minutes: 'minutos',
        seconds: 'segundos'
    }
};
        
$.fn.durationPicker = function (options) {
    	
    	// Store an instance of moment duration
        var totalDuration = 0;       
        
        var defaults = {
            lang: 'en',
            max: 59,
            checkRanges: false,
            totalMax: 31556952000, // 1 year
            totalMin: 60000, // 1 minute
            showSeconds: false,
            showDays: true
        };
        
        var settings = $.extend( {}, defaults, options );
                
        this.each(function (i, mainInput) {

            var $mainInput = $(mainInput);

            if ($mainInput.data('bdp') === '1') {
            	return;
            }

            function buildDisplayBlock(id, hidden) {
                return '<div class="bdp-block '+ (hidden ? 'hidden' : '') + '">' +
                            '<span id="bdp-'+ id +'"></span><br>' +
                            '<span class="bdp-label" id="' + id + '_label"></span>' +
                        '</div>';
            }

            var $mainInputReplacer = $('<div class="bdp-input"></div>');
            $mainInputReplacer.append(buildDisplayBlock('days', !settings.showDays));
            $mainInputReplacer.append(buildDisplayBlock('hours'));
            $mainInputReplacer.append(buildDisplayBlock('minutes'));
            $mainInputReplacer.append(buildDisplayBlock('seconds', !settings.showSeconds));
            
            $mainInput.after($mainInputReplacer).hide().data('bdp', '1');

            var inputs = [];

            var disabled = false;
            if ($mainInput.hasClass('disabled') || $mainInput.attr('disabled') == 'disabled') {
                disabled = true;
                $mainInputReplacer.addClass('disabled');
            }

            function updateMainInput() {
                $mainInput.val(totalDuration.asMilliseconds());
                $mainInput.change(); 
            }

            function updateMainInputReplacer() {            	
                $mainInputReplacer.find('#bdp-days').text(totalDuration.days());
                $mainInputReplacer.find('#bdp-hours').text(totalDuration.hours());
                $mainInputReplacer.find('#bdp-minutes').text(totalDuration.minutes());
                $mainInputReplacer.find('#bdp-seconds').text(totalDuration.seconds());

                $mainInputReplacer.find('#days_label').text(langs[settings.lang][totalDuration.days() == 1 ? 'day' : 'days']);
                $mainInputReplacer.find('#hours_label').text(langs[settings.lang][totalDuration.hours() == 1 ? 'hour' : 'hours']);
                $mainInputReplacer.find('#minutes_label').text(langs[settings.lang][totalDuration.minutes() == 1 ? 'minute' : 'minutes']);
                $mainInputReplacer.find('#seconds_label').text(langs[settings.lang][totalDuration.seconds() == 1 ? 'second' : 'seconds']);
            }

            function updatePicker() {
                if (disabled) {
                	return;
                }
                // Array of jQuery object inputs
                inputs.days.val(totalDuration.days());
                inputs.hours.val(totalDuration.hours());
                inputs.minutes.val(totalDuration.minutes());
                inputs.seconds.val(totalDuration.seconds());
            }

            function init() {
                if (!$mainInput.val()) {
                	$mainInput.val(0);
                }

                // Initialize moment with locale                
                moment.locale(settings.lang);

                totalDuration = moment.duration(parseInt($mainInput.val(), 10));
                checkRanges();
                updatePicker();
            }

            function picker_changed() {
            	totalDuration = moment.duration({
            		seconds : parseInt(inputs.seconds.val()),
            		minutes : parseInt(inputs.minutes.val()),
            		hours : parseInt(inputs.hours.val()),
            		days :  parseInt(inputs.days.val())
            	});
            	checkRanges();
            	updateMainInput();
            }

            function buildNumericInput(label, hidden, max) {
                var $input = $('<input class="form-control input-sm" type="number" min="0" value="0">')
                    .change(picker_changed);
                if (max) {
                    $input.attr('max', max);
                }
                inputs[label] = $input;
                var $ctrl = $('<div> ' + langs[settings.lang][label] + '</div>');
                if (hidden) {
                    $ctrl.addClass('hidden');
                }
                return $ctrl.prepend($input);
            }
                        
            function checkRanges() {
            	if (settings.checkRanges) {            		
            		// Assign max value if out of range
            		totalDuration = (totalDuration.asMilliseconds() > settings.totalMax) ? moment.duration(settings.totalMax) : totalDuration;            		
            		// Assign minimum value if out of range
            		totalDuration = (totalDuration.asMilliseconds() < settings.totalMin) ? moment.duration(settings.totalMin) : totalDuration;            		  
            	}
            	// Always update input replacer
            	updateMainInputReplacer();
            }

            if (!disabled) {
                var $picker = $('<div class="bdp-popover"></div>');
                buildNumericInput('days', !settings.showDays).appendTo($picker);
                buildNumericInput('hours', false, 23).appendTo($picker);
                buildNumericInput('minutes', false, 59).appendTo($picker);
                buildNumericInput('seconds', !settings.showSeconds, 59).appendTo($picker);

                $mainInputReplacer.popover({
                    placement: 'bottom',
                    trigger: 'click',
                    html: true,
                    content: $picker
                });
            }
            init();
            $mainInput.change(init);
        });
    };
}(jQuery));
