var ClaimMainApp = angular.module('ClaimApplication', [])



ClaimMainApp.factory('viewModelHelper', function ($http, $q) {
    return ClaimObj.viewModelHelper($http, $q);
});

ClaimMainApp.factory('validator', function () {
    return valJs.validator();
});

(function (cr) {
    var viewModelHelper = function ($http, $q) {

        var self = this;

        self.modelIsValid = true;
        self.modelErrors = [];
        self.isLoading = false;

        self.apiGet = function (uri, data, success, failure, always) {
            self.isLoading = true;
            self.modelIsValid = true;
            $http.get(ClaimObj.rootPath + uri, data)
                .then(function (result) {
                    success(result);
                    if (always != null)
                        always();
                    self.isLoading = false;
                }, function (result) {
                    if (failure == null) {
                        if (result.status != 400 && result.status != 404 && result.status != 500)
                            self.modelErrors = [result.status + ':' + result.statusText + ' - ' + result.data];
                        else
                            self.modelErrors = [result.data];
                        self.modelIsValid = false;
                    } else
                        failure(result);
                    if (always != null)
                        always();
                    self.isLoading = false;
                });
        };

        self.apiPost = function (uri, data, success, failure, always) {
            self.isLoading = true;
            self.modelIsValid = true;
            $http.post(ClaimObj.rootPath + uri, data)
                .then(function (result) {
                    success(result);
                    if (always != null)
                        always();
                    self.isLoading = false;
                }, function (result) {
                    if (failure == null) {
                        if (result.status != 400 && result.status != 404 && result.status != 500)
                            self.modelErrors = [result.status + ':' + result.statusText + ' - ' + result.data];
                        else
                            self.modelErrors = [result.data];
                        self.modelIsValid = false;
                    } else
                        failure(result);
                    if (always != null)
                        always();
                    self.isLoading = false;
                });
        };

        return this;
    };
    cr.viewModelHelper = viewModelHelper;
} (window.ClaimObj));

(function (cr) {
    var mustEqual = function (value, other) {
        return value == other;
    };
    cr.mustEqual = mustEqual;
}(window.ClaimObj));

(function (cr) {
    var expireDateForMonth = function (value, other) {
        return (!(parseInt(value) < (new Date().getMonth() + 1) && parseInt(other) <= (new Date().getFullYear())));
    };
    cr.expireDateForMonth = expireDateForMonth;
}(window.ClaimObj));

(function (cr) {
    var validEmail = function (value) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(value);
    };
    cr.validEmail = validEmail;
}(window.ClaimObj));

(function (cr) {
    var validDate = function (value, other) {
        var re = /^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/;
        if (other != undefined) {
            if (typeof (other) != 'string') {
                other = other.format("dd/mm/yyyy");
            }
        }
        return re.test(other);
    };
    cr.validDate = validDate;
}(window.ClaimObj));

(function (cr) {
    var validPostalCode = function (value) {
        var re = /^[a-zA-Z0-9]+ [a-zA-Z0-9]+$/;
        return re.test(value);
    };
    cr.validPostalCode = validPostalCode;
}(window.ClaimObj));

(function (cr) {
    var validMobile = function (value) {
        var re = /^[0-9]*$/;
        return re.test(value);
    };
    cr.validMobile = validMobile;
}(window.ClaimObj));




(function (cr) {
    var validCreditCard = function (ccnumber) {
        var len = ccnumber.length;
        mul = 0,
        prodArr = [[0, 1, 2, 3, 4, 5, 6, 7, 8, 9], [0, 2, 4, 6, 8, 1, 3, 5, 7, 9]],
        sum = 0;

        while (len--) {
            sum += prodArr[mul][parseInt(ccnumber.charAt(len), 10)];
            mul ^= 1;
        }

        if (sum % 10 === 0 && sum > 0) {
            valid = true;
        } else {
            valid = false;
        }
        return valid;
    };
    cr.validCreditCard = validCreditCard;
}(window.ClaimObj));


(function (cr) {
    var trueValue = function (value) {
        return (value == true);
    };
    cr.trueValue = trueValue;
}(window.ClaimObj));

// ***************** validation *****************
//(function (cr) {
//    var indexOf = function (needle) {
//        if (typeof Array.prototype.indexOf === 'function') {
//            indexOf = Array.prototype.indexOf;
//        } else {
//            indexOf = function (needle) {
//                var i = -1, index = -1;

//                for (i = 0; i < this.length; i++) {
//                    if (this[i] === needle) {
//                        index = i;
//                        break;
//                    }
//                }

//                return index;
//            };
//        }
//        return indexOf.call(this, needle);
//    };
//    cr.indexOf = indexOf;
//} (window.ClaimObj));

window.valJs = {};

(function (val) {
    var validator = function () {

        var self = this;

        self.PropertyRule = function (propertyName, rules) {
            var self = this;
            self.PropertyName = propertyName;
            self.Rules = rules;
        };

        self.ValidateModel = function (model, allPropertyRules) {
            var errors = [];
            var props = Object.keys(model);
            for (var i = 0; i < props.length; i++) {
                var prop = props[i];
                for (var j = 0; j < allPropertyRules.length; j++) {
                    var propertyRule = allPropertyRules[j];
                    if (prop == propertyRule.PropertyName) {
                        var propertyRules = propertyRule.Rules;

                        var propertyRuleProps = Object.keys(propertyRules);
                        for (var k = 0; k < propertyRuleProps.length; k++) {
                            var propertyRuleProp = propertyRuleProps[k];
                            if (propertyRuleProp != 'custom' && propertyRuleProp != 'pattern') {
                                var rule = rules[propertyRuleProp];
                                var params = null;
                                if (propertyRules[propertyRuleProp].hasOwnProperty('params'))
                                    params = propertyRules[propertyRuleProp].params;
                                var validationResult = rule.validator(model[prop], params);
                                if (!validationResult) {
                                    errors.push(getMessage(prop, propertyRules[propertyRuleProp], rule.message));
                                }
                            } else if (propertyRuleProp == 'pattern') {
                                if (model[prop] + "" != "") {
                                    var rule = rules[propertyRuleProp];
                                    var params = null;
                                    if (propertyRules[propertyRuleProp].hasOwnProperty('params'))
                                        params = propertyRules[propertyRuleProp].params;
                                    var validationResult = rule.validator(model[prop], params);
                                    if (!validationResult) {
                                        errors.push(getMessage(prop, propertyRules[propertyRuleProp], rule.message));
                                    }
                                } else {
                                    if (ClaimObj.indexOf.call(propertyRuleProps, "required") > -1) {
                                        var rule = rules[propertyRuleProp];
                                        var params = null;
                                        if (propertyRules[propertyRuleProp].hasOwnProperty('params'))
                                            params = propertyRules[propertyRuleProp].params;
                                        var validationResult = rule.validator(model[prop], params);
                                        if (!validationResult) {
                                            errors.push(getMessage(prop, propertyRules[propertyRuleProp], rule.message));
                                        }
                                    }
                                }
                            } else {
                                if (model[prop] + "" != "") {
                                    var validator = propertyRules.custom.validator;
                                    var value = null;
                                    if (propertyRules.custom.hasOwnProperty('params')) {
                                        value = propertyRules.custom.params;
                                    }
                                    var result = validator(model[prop], value());
                                    if (result != true) {
                                        errors.push(getMessage(prop, propertyRules.custom, 'Invalid value.'));
                                    }
                                } else {
                                    if (ClaimObj.indexOf.call(propertyRuleProps, "required") > -1) {
                                        var validator = propertyRules.custom.validator;
                                        var value = null;
                                        if (propertyRules.custom.hasOwnProperty('params')) {
                                            value = propertyRules.custom.params;
                                        }
                                        var result = validator(model[prop], value());
                                        if (result != true) {
                                            errors.push(getMessage(prop, propertyRules.custom, 'Invalid value.'));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            model['errors'] = errors;
            model['isValid'] = (errors.length == 0);
        };

        var getMessage = function (prop, rule, defaultMessage) {
            var message = '';
            if (rule.hasOwnProperty('message'))
                message = rule.message;
            else
                message = prop + ': ' + defaultMessage;
            return message;
        };

        var rules = [];

        var setupRules = function () {

            rules['required'] = {
                validator: function (value, params) {
                    return !(value == null || ((value + "").trim() == ''));
                },
                message: 'Value is required.'
            };
            rules['minLength'] = {
                validator: function (value, params) {
                    return !(value.trim().length < params);
                },
                message: 'Value does not meet minimum length.'
            };
            rules['pattern'] = {
                validator: function (value, params) {
                    var regExp = new RegExp(params);
                    return !(regExp.exec(value.trim()) == null);
                },
                message: 'Value must match regular expression.'
            };
            rules['hoursWithin24'] = {
                validator: function (value, params) {
                    if (value != null) {
                        var allHours = value;
                        var diff = allHours.split(".");
                        var hours = diff[0];
                        var minutes = diff[1];
                        if ((hours > 24) || (hours <= 0)) {
                            return false;
                        } else if ((hours == 24) && (minutes > 0)) {
                            return false;
                        } else {
                            return true;
                        }
                    } else {
                        return true;
                    }
                },
                message: 'Value must be between 0 and 24.'
            };
        };

        setupRules();

        return this;
    };
    val.validator = validator;
} (window.valJs));