;
UTIL = (function () {
	var fixJsonDate = function (date) {
			var jsonDateRegex = /\/Date\((.*?)\)\//i,
				matches, intValue;
			
			if (!(jsonDateRegex.test(date))) {
				throw new Error('Date is not in json format.');
			}
			matches = date.match(jsonDateRegex);
			intValue = parseInt(matches[1], 10);
			return new Date(Date(intValue));
		};
		
	/* public interface */
	return {
		fixJsonDate: fixJsonDate
	};
}());
