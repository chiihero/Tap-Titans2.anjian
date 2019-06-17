var winner_e = '';
var winner_e10 = '';
var winner_e100 = '';
var winner_e1000 = '';
var winner_n = '';
var winner_n_e = '';
var winner_s1 = '';
var winner_s2 = '';
var winner_s3 = '';
var winner_s4 = '';
var winner_s5 = '';
var winner_value = 0;
var winner_value10 = 0;
var winner_value100 = 0;
var winner_value1000 = 0;
var winner_svalue = 0;
var obfuscate = 0;
var white_rabbit = 0;
var comeUndone = '';
var recalc_litmus = true;
var halp = ('1' == getURLParameter('halp') ? true : false);

function updateRecalc() {
    if ($('#recalc_on').prop('checked') == true) {
        $('#btnrecalcon').removeClass('btn-secondary').addClass('btn-primary');
        $('#btnrecalcoff').removeClass('btn-primary').addClass('btn-secondary');
        recalc_litmus = true;
    } else {
        $('#btnrecalcoff').removeClass('btn-secondary').addClass('btn-primary');
        $('#btnrecalcon').removeClass('btn-primary').addClass('btn-secondary');
        recalc_litmus = false;
    }
}

function getURLParameter(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}

function toggleSplash(reweight) {
    if ($('#wet').prop('checked') == true) {
        $('#btnwet').removeClass('btn-dark text-secondary').addClass('btn-info');
        $('#btndry').removeClass('btn-info').addClass('btn-dark text-secondary');
    } else {
        $('#btndry').removeClass('btn-dark text-secondary').addClass('btn-info');
        $('#btnwet').removeClass('btn-info').addClass('btn-dark text-secondary');
    }
    if (false != reweight) {
        adjustWeights(true);
    }
}

function generateArtifacts() {
    $('#accept2').hide();
    $('#artifacts').empty();
    $('#daltifacts').empty();
    $.each(artifacts.data, function (k, v) {
        if (isNaN(v.level)) {
            v.level = 0;
        }
        var row = '<tr class="' + (1 == v.active ? '' : 'text-dark bg-secondary') + '" id="' + k + 'row">';
        row += '<td>';
        row += '<input type="checkbox" aria-label="Checkbox to designate active status for ' + v.name + '" id="' + k + 'active"' + (v.active == 1 ? ' checked="checked"' : '') + ' onchange="updateActive(\'' + k + '\');" tabindex="-1">';
        row += '</td>';
        row += '<td>';
        row += '<label for="' + k + 'active" id="basic-addon' + k + '">';
        row += '<span class="d-block d-sm-none">' + v.name + '</span>';
        row += '<span class="d-none d-sm-block">' + v.name + '</span>';
        row += '</label>';
        row += '</td>';
        row += '<td>';
        row += '<input id="' + k + '" value="' + (999999999999999 < v.level ? displayTruncated(v.level) : avoidSci(v.level)) + '" type="number" class="form-control artlvl" placeholder="0" aria-label="Level of ' + v.name + '" aria-describedby="basic-addon' + k + '"onchange="updateArtifact(\'' + k + '\')">';
        row += '</td>';
        row += '<td>';
        row += '<span class="badge" id="' + k + 'expo"></span>';
        row += '</td>';
        row += '<td>';
        row += '<button class="badge badge-secondary" type="button" data-toggle="collapse" data-target="#' + k + 'info" aria-expanded="false" aria-controls="' + k + 'info" tabindex="-1">&#x00A0;i&#x00A0;</button>';
        row += '</td>';
        row += '</tr>';
        row += '<tr class="collapse" id="' + k + 'info">';
        row += '<td colspan="5">';
        row += '<dl class="row">';
        row += '<dt class="col-3 col-sm-6 text-right">Name</dt>';
        row += '<dd class="col-9 col-sm-6">' + v.name + '</dd>';
        row += '<dt class="col-3 col-sm-6 text-right">Effect</dt>';
        row += '<dd class="col-9 col-sm-6" id="' + k + 'effect"></dd>';
        row += '<dt class="col-3 col-sm-6 text-right">';
        row += '<span class="d-block d-sm-none">AD</span>';
        row += '<span class="d-none d-sm-block">Artifact Damage</span>';
        row += '</dt>';
        row += '<dd class="col-9 col-sm-6" id="' + k + 'ad"></dd>';
        row += '<dt class="col-3 col-sm-6 text-right">';
        row += '<span class="d-block d-sm-none">Cost</span>';
        row += '<span class="d-none d-sm-block">Cost to Upgrade</span>';
        row += '</dt>';
        row += '<dd class="col-9 col-sm-6" id="' + k + 'cost"></dd>';
        row += '<dt class="col-3 col-sm-6 text-right">';
        row += '<span class="d-block d-sm-none">Effc.</span>';
        row += '<span class="d-none d-sm-block">Efficiency</span>';
        row += '</dt>';
        row += '<dd class="col-9 col-sm-6" id="' + k + 'eff"></dd>';
        row += '</dl>';
        row += '</td>';
        row += '</tr>';
        $('#artifacts').append(row);
        var div = '<div class="col-3 col-sm-2 col-lg-1 border text-center">';
        div += '<strong>' + v.name + '</strong><br><span id="' + k + 'dalt">' + displayTruncated(v.level) + '</span>';
        div += '</div>'
        $('#daltifacts').append(div);
    });
}

function determineTextColor(branch) {
    switch (branch) {
        case 'red':
            return 'text-danger';
            break;

        case 'yellow':
            return 'text-warning';
            break;

        case 'blue':
            return 'text-primary';
            break;

        case 'green':
            return 'text-success';
            break;
    }
}

function generateSkills() {
    $('#skills').empty();
    $.each(skills.data, function (k, v) {
        if (isNaN(v.level)) {
            v.level = 0;
        }
        var row = '<tr class="' + (1 == v.active ? '' : 'text-dark bg-secondary') + '" id="skill' + k + 'row">';
        row += '<td>';
        row += '<input type="checkbox" aria-label="Checkbox to designate active status for ' + v.name + '" id="skill' + k + 'active"' + (v.active == 1 ? ' checked="checked"' : '') + ' onchange="updateActiveSkill(\'' + k + '\');" tabindex="-1">';
        row += '</td>';
        row += '<td>';
        row += '<label for="skill' + k + 'active" id="basic-addonskill' + k + '">';
        row += '<span class="d-block d-sm-none ' + determineTextColor(v.branch) + '">' + v.name + '</span>';
        row += '<span class="d-none d-sm-block ' + determineTextColor(v.branch) + '">' + v.name + '</span>';
        row += '</label>';
        row += '</td>';
        row += '<td>';
        row += '<input id="skill' + k + '" value="' + v.level + '" type="tel" class="form-control artlvl" placeholder="0" aria-label="Level of ' + v.name + '" aria-describedby="basic-addonskill' + k + '"onchange="updateSkill(\'' + k + '\')">';
        row += '</td>';
        row += '<td>';
        row += '<span class="badge" id="skill' + k + 'expo"></span>';
        row += '</td>';
        row += '<td>';
        row += '<button class="badge badge-secondary" type="button" data-toggle="collapse" data-target="#skill' + k + 'info" aria-expanded="false" aria-controls="skill' + k + 'info" tabindex="-1">&#x00A0;i&#x00A0;</button>';
        row += '</td>';
        row += '</tr>';
        row += '<tr class="collapse" id="skill' + k + 'info">';
        row += '<td colspan="5">';
        row += '<dl class="row">';
        row += '<dt class="col-3 col-sm-6 text-right">Name</dt>';
        row += '<dd class="col-9 col-sm-6">' + v.name + '</dd>';
        row += '<dt class="col-3 col-sm-6 text-right">Effect</dt>';
        row += '<dd class="col-9 col-sm-6" id="skill' + k + 'effect"></dd>';
        row += '<dt class="col-3 col-sm-6 text-right">Effect2</dt>';
        row += '<dd class="col-9 col-sm-6" id="skill' + k + 'effect2"></dd>';
        row += '<dt class="col-3 col-sm-6 text-right">Effect3</dt>';
        row += '<dd class="col-9 col-sm-6" id="skill' + k + 'effect3"></dd>';
        row += '<dt class="col-3 col-sm-6 text-right">';
        row += '<span class="d-block d-sm-none">Cost</span>';
        row += '<span class="d-none d-sm-block">Cost to Upgrade</span>';
        row += '</dt>';
        row += '<dd class="col-9 col-sm-6" id="skill' + k + 'cost"></dd>';
        row += '<dt class="col-3 col-sm-6 text-right">';
        row += '<span class="d-block d-sm-none">Effc.</span>';
        row += '<span class="d-none d-sm-block">Efficiency</span>';
        row += '</dt>';
        row += '<dd class="col-9 col-sm-6" id="skill' + k + 'eff"></dd>';
        row += '</dl>';
        row += '</td>';
        row += '</tr>';
        $('#skills').append(row);
    });
}

function adjustBoS() {
    var expo = 0
    $.each(artifacts.data, function (k, v) {
        if ('bos' != k && 1 == v.active && -1 == v.max) {
            expo += v.rating;
        }
    });
    artifacts.data.bos.rating = expo;
    artifacts = calculate(artifacts, 'bos', true, true);
}

function updateActive(k) {
    if ($('#' + k + 'active').is(':checked')) {
        artifacts.data[k].active = 1;
        $('#' + k + 'row').removeClass('text-dark bg-secondary');
    } else {
        artifacts.data[k].active = 0;
        $('#' + k + 'row').addClass('text-dark bg-secondary');
    }
    adjustBoS();
    artifacts = calculate(artifacts, k, true, true);
}

function updateActiveSkill(k) {
    if ($('#skill' + k + 'active').is(':checked')) {
        skills.data[k].active = 1;
        $('#skill' + k + 'row').removeClass('text-dark bg-secondary');
    } else {
        skills.data[k].active = 0;
        $('#skill' + k + 'row').addClass('text-dark bg-secondary');
    }
    calculateAllSkills();
}

function checkAllArtifacts() {
    $.each(artifacts.data, function (k, v) {
        $('#' + k + 'active').prop('checked', true);
        artifacts.data[k].active = 1;
        $('#' + k + 'row').removeClass('text-dark bg-secondary');
        $('#' + k).prop('readonly', false);
    });
    artifacts = calculateAll(artifacts, true);
}

function checkAllSkills() {
    $.each(skills.data, function (k, v) {
        $('#skill' + k + 'active').prop('checked', true);
        skills.data[k].active = 1;
        $('#skill' + k + 'row').removeClass('text-dark bg-secondary');
        $('#skill' + k).prop('readonly', false);
    });
    calculateAllSkills();
}

function resetSkills() {
    $.each(skills.data, function (k, v) {
        skills.data[k].level = 0;
    });
    calculateSkillTotals();
    calculateAllSkills();
}

function dalViewArtifact(litmus) {
    if (litmus) {
        $('#dal-tab').tab('show');
    } else {
        $('#artifacts-tab').tab('show');
    }
}

function regenerateArtifacts() {
    $.each(artifacts.data, function (k, v) {
        if (isNaN(v.level)) {
            v.level = 0;
        }
        $('#' + k).val((999999999999999 < v.level ? displayTruncated(v.level) : avoidSci(v.level)));
        $('#' + k + 'dalt').text(displayTruncated(v.level));
        var value = '';
        if (0 < v.level && undefined != v.current_effect) {
            value = displayEffect(v.current_effect, v.type);
        }
        value += v.bonus;
        $('#' + k + 'effect').empty().append(value);
        value = '';
        if (0 < v.level && undefined != v.current_ad) {
            value = displayPct(v.current_ad);
        }
        $('#' + k + 'ad').empty().append(value);
        value = '';
        if (1 == v.active && undefined != v.displayCost) {
            value = v.displayCost + ' Relics';
        }
        $('#' + k + 'cost').empty().append(value);
        value = '';
        if (1 == v.active && undefined != v.efficiency && '' != v.efficiency) {
            value = v.efficiency.toExponential(12);
        }
        $('#' + k + 'eff').empty().append(value);
        value = '';
        if (undefined != v.rating) {
            value = v.rating.toFixed(2).replace(/\.?0+$/, '');
        }
        $('#' + k + 'expo').empty().append(value).removeClass().addClass('badge').addClass('badge-' + v.color);
    });
    storeData();
}

function regenerateSkills() {
    $.each(skills.data, function (k, v) {
        if (isNaN(v.level)) {
            v.level = 0;
        }
        $('#skill' + k).val(v.level);
        $('#' + v.nickname).text(v.level);
        var value = '';
        if (0 < v.level && undefined != v.current_effect) {
            value = displayEffect(v.current_effect, v.type);
        }
        value += v.bonus;
        $('#skill' + k + 'effect').empty().append(value);
        var value = '';
        if (0 < v.level && undefined != v.current_effect2 && false != v.current_effect2 && -1 != v.current_effect2) {
            value = displayEffect(v.current_effect2, v.type2);
        }
        value += (-1 != v.bonus2 ? v.bonus2 : '');
        $('#skill' + k + 'effect2').empty().append(value);
        var value = '';
        if (0 < v.level && undefined != v.current_effect3 && false != v.current_effect3 && -1 != v.current_effect3) {
            value = displayEffect(v.current_effect3, v.type3);
        }
        value += (-1 != v.bonus3 ? v.bonus3 : '');
        $('#skill' + k + 'effect3').empty().append(value);
        value = '';
        if (1 == v.active && undefined != v.cost) {
            value = v.cost + ' SP';
        }
        $('#skill' + k + 'cost').empty().append(value);
        value = '';
        if (1 == v.active && undefined != v.efficiency && '' != v.efficiency) {
            value = v.efficiency;
        }
        $('#skill' + k + 'eff').empty().append(value);
        value = '';
        if (undefined != v.rating1) {
            value = v.rating1.toFixed(2).replace(/\.?0+$/, '');
        }
        if (undefined != v.rating2) {
            value += '/' + v.rating2.toFixed(2).replace(/\.?0+$/, '');
        }
        if (undefined != v.rating3) {
            value += '/' + v.rating3.toFixed(2).replace(/\.?0+$/, '');
        }
        $('#skill' + k + 'expo').empty().append(value).removeClass().addClass('badge').addClass('badge-' + ('info' == v.color ? 'success' : v.color));
    });
    storeData();
}

function updateArtifact(k) {
    artifacts.data[k].level = parseFloat($('#' + k).val());
    artifacts.totalAD = calculateTotalAD(artifacts.data, true);
    if (true == recalc_litmus) {
        adjustWeights(true);
    } else {
        storeData();
    }
}

function updateSkill(k) {
    var lvl = parseInt($('#skill' + k).val());
    skills.data[k].level = (skills.data[k].max < lvl ? skills.data[k].max : lvl);
    $('#skill' + k).val(lvl);
    adjustWeights(false);
}

function countArtifacts(data) {
    var i = 0;
    $.each(data, function (k, v) {
        if (v.level > 0) {
            i++;
        }
    });
    return (i);
}

function determineAverage(data) {
    var i = countArtifacts(data);
    var x = 0;
    var y = 0;
    $.each(data, function (k, v) {
        obfuscate++;
        if (v.level > 0) {
            x += v.level;
        }
    });
    if (i > 0 && x > 0) {
        y = x / i;
    }
    return (y);
}

function determineArtifactStepWinner(step) {
    var temp_winner = '';
    switch (step) {
        case 1:
            temp_winner = winner_e;
            break;
        case 10:
            temp_winner = winner_e10;
            break;
        case 100:
            temp_winner = winner_e100;
            break;
        case 1000:
            temp_winner = winner_e1000;
            break;
    }
    return (temp_winner);
}

function determineArtifactStep(v, step) {
    var interval = 0;
    switch (step) {
        case 1:
            interval = 1;
            break;
        case 10:
            interval = v.efficiency10_int;
            break;
        case 100:
            interval = v.efficiency100_int;
            break;
        case 1000:
            interval = v.efficiency1000_int;
            break;
    }
    return (interval);
}

function determineArtifactCost(v, step) {
    var cost = 0;
    switch (step) {
        case 1:
            cost = v.cost;
            break;
        case 10:
            cost = v.efficiency10_cost;
            break;
        case 100:
            cost = v.efficiency100_cost;
            break;
        case 1000:
            cost = v.efficiency1000_cost;
            break;
    }
    return (cost);
}

function dowsingRod(v, unit, relics, lvlDiff = 0) {
    var count = 0;
    var cost = calculateArtifactEfficiencyCost(v, unit);
    if (relics < cost) {
        return (count);
    }
    obfuscate++;
    cost = calculateArtifactEfficiencyCost(v, unit - lvlDiff);
    relics -= cost;
    count++;
    v.level += unit - lvlDiff;
    while (0 < relics && (0 != cost || count === 1)) {
        obfuscate++;
        cost = calculateArtifactEfficiencyCost(v, unit);
        relics -= cost;
        if (0 < relics) {
            count++;
            v.level += unit;
        }
    }
    return (count * unit - lvlDiff);
};

function processPct(k, v, relics, totalAD, tattoo) {
    var current_ad = v.level * v.ad;
    var current_effect = 1 + v.effect * Math.pow(v.level, Math.pow((1 + (v.cexpo - 1) * Math.min(v.grate * v.level, v.gmax)), v.gexpo));
    switch (v.dime) {
        case 0:
            break;
        case 1:
            if (0 < artifacts.data.tmg.level) {
                current_effect *= 10;
            }
            break;
        case 2:
            if (0 < artifacts.data.ttof.level) {
                current_effect *= 10;
            }
            break;
    }
    var levels = 0;
    var cost = 0;
    var total_cost = 0;
    var dowse = 0;
    var running_dowse = 0;
    var orig_level = v.level;
    var fresh = false;
    if (1 == v.active) {
        if (0 == v.level) {
            fresh = true;
            orig_level = 1;
            var next_artifact = countArtifacts(artifacts.data) + 1;
            total_cost = artifact_costs[next_artifact];
        }
        if (-1 == v.max) {
            var lvledUnit = 1000000000000000000000000000000000000000000000000000000000;
            var lvledLvl = v.level;
            while (lvledUnit > 0.1 && dowse === 0) {
                if (lvledLvl > lvledUnit) {
                    lvledLvl %= lvledUnit;
                }
                dowse = dowsingRod(v, lvledUnit, relics, lvledLvl);
                v.level = orig_level;
                running_dowse += dowse;
                cost = calculateArtifactEfficiencyCost(v, dowse);
                total_cost += cost;
                relics -= cost;
                v.level += dowse;
                orig_level = v.level;
                lvledUnit /= 10;
            }
            if (true == tattoo && false == fresh) {
                u_relics -= total_cost;
                upgrades.steps.push({
                    'k': k,
                    'levels': running_dowse,
                    'cost': total_cost,
                    'remaining': u_relics
                });
                return (running_dowse);
            } else if (0 < running_dowse) {
                return (calculateArtifactEfficiency(v, total_cost, running_dowse, current_ad, current_effect, totalAD));
            } else {
                return (-1);
            }
        } else if (v.max > v.level) {
            var levels = v.max - v.level;
            while (0 < levels) {
                obfuscate++;
                cost = calculateArtifactEfficiencyCost(v, levels);
                if (cost <= relics) {
                    if (true == tattoo && false == fresh) {
                        u_relics -= cost;
                        upgrades.steps.push({
                            'k': k,
                            'levels': levels,
                            'cost': cost,
                            'remaining': u_relics
                        })
                        return (levels);
                    } else if (0 < levels) {
                        return (calculateArtifactEfficiency(v, cost, levels, current_ad, current_effect, totalAD));
                    } else {
                        return (-1);
                    }
                } else {
                    levels--
                }
            }
        }
    }
    return (-1);
}

function optimizePct() {
    obfuscate++;
    var winnerPct = '';
    var winnerPct_value = 0;
    var temp_value = 0;
    var temp_new_value = 0;
    var relics_pct = Math.floor(u_relics * (u_step / 100));
    $.each(u_temp_artifacts.data, function (k, v) {
        obfuscate++;
        var orig_level = v.level;
        temp_value = processPct(k, v, relics_pct, u_temp_artifacts.totalAD, false);
        v.level = orig_level;
        if (temp_value > winnerPct_value) {
            if (0 == v.level) {
                if (temp_value > temp_new_value && ('' == winner_n_e || winner_n_e < v.rating)) {
                    winner_n = k;
                    winner_n_e = v.rating;
                    temp_new_value = temp_value;
                }
            } else {
                winnerPct = k;
                winnerPct_value = temp_value;
            }
        }
    });
    if ('' != winnerPct) {
        var orig_level = u_temp_artifacts.data[winnerPct].level;
        var dowse = processPct(winnerPct, u_temp_artifacts.data[winnerPct], relics_pct, u_temp_artifacts.totalAD, true);
        u_temp_artifacts.data[winnerPct].level = orig_level;
        u_temp_artifacts.data[winnerPct].level += dowse;
        u_temp_artifacts.totalAD = calculateTotalAD(u_temp_artifacts.data, false);
    }
    if ('' != winnerPct && u_relics >= u_threshhold) {
        var progress = (1 - (u_relics > 0 ? u_relics / (u_orelics - u_threshhold) : 0 / u_orelics)) * 100;
        $('#progress').width(progress + '%');
        $('#progress').prop('aria-valuenow', progress);
        buffer = u_obuffer;
        window.setTimeout(optimizePct, 1);
    } else {
        var progress = 100;
        $('#progress').width(progress + '%');
        $('#progress').prop('aria-valuenow', progress);
        $('#progress').removeClass('progress-bar-striped progress-bar-animated');
        renderPctSuggestions(u_temp_artifacts);
    }
}

function optimize() {
    var temp_winner = determineArtifactStepWinner(u_step);
    var temp_step = determineArtifactStep(u_temp_artifacts.data[temp_winner], u_step);
    var temp_cost = determineArtifactCost(u_temp_artifacts.data[temp_winner], u_step);
    while (buffer-- > 0 && u_relics >= temp_cost) {
        obfuscate++;
        if (undefined == upgrades[temp_winner]) {
            upgrades[temp_winner] = temp_step;
        } else {
            upgrades[temp_winner] += temp_step;
        }
        u_relics -= temp_cost;
        if (undefined == u_temp_artifacts.data[temp_winner].upgradeCost) {
            u_temp_artifacts.data[temp_winner].upgradeCost = temp_cost;
        } else {
            u_temp_artifacts.data[temp_winner].upgradeCost += temp_cost;
        }
        u_temp_artifacts.data[temp_winner].level += temp_step;
        u_temp_artifacts = calculate(u_temp_artifacts, temp_winner, false, false);
        temp_winner = determineArtifactStepWinner(u_step);
        temp_step = determineArtifactStep(u_temp_artifacts.data[temp_winner], u_step);
        temp_cost = determineArtifactCost(u_temp_artifacts.data[temp_winner], u_step);
    }
    if (u_relics >= temp_cost) {
        var progress = (1 - (u_relics > 0 ? u_relics / u_orelics : 0 / u_orelics)) * 100;
        $('#progress').width(progress + '%');
        $('#progress').prop('aria-valuenow', progress);
        buffer = u_obuffer;
        window.setTimeout(optimize, 1);
    } else {
        var progress = 100;
        $('#progress').width(progress + '%');
        $('#progress').prop('aria-valuenow', progress);
        $('#progress').removeClass('progress-bar-striped progress-bar-animated');
        renderSuggestions(u_temp_artifacts);
    }
}

function generateUpgrades() {
    if (false == recalc_litmus) {
        adjustWeights(true);
    }
    obfuscate = 0;
    white_rabbit = new Date();
    $('#export_wrap').hide();
    $('#import_wrap').hide();
    $('#new_artifact').empty();
    $('#pudding').empty();
    $('#accept').empty();
    $('#suggestions').empty();
    $('#progressBar').hide();
    $('#progress').width('0%');
    $('#progress').prop('aria-valuenow', 0);
    $('#progress').addClass('progress-bar-striped progress-bar-animated');
    $('#progressBar').show();
    $('#relicsuggs').show();
    $('#relicreccs').hide();
    if (null == $('#ocd').val()) {
        $('#ocd').val('1');
    }
    storeData();
    var quickCheck = 0;
    $.each(artifacts.data, function (k, v) {
        if (0 == v.level && v.rating >= 3 && v.rating > quickCheck && "1" == v.active) {
            quickCheck = v.rating;
            winner_n = k;
        }
    });
    u_relics = new Decimal(('' == $('#relics').val() ? 0 : $('#relics').val()) + '.' + ('' == $('#relics_decimal').val() ? 0 : $('#relics_decimal').val()));
    buffer = Math.pow(10, $('#relic_factor').val().substr(1));
    u_relics = u_relics.mul(buffer).toNumber();
    u_orelics = u_relics;
    u_obuffer = buffer;
    u_threshhold = new Decimal(('' == $('#spend').val() ? 95 : $('#spend').val()) + '.' + ('' == $('#spend_decimal').val() ? 0 : $('#spend_decimal').val()));
    u_threshhold = 1 - u_threshhold.div(100).toNumber();
    u_threshhold *= u_relics;
    upgrades = {};
    u_temp_artifacts = $.extend(true, {}, artifacts);
    var litmus = false;
    $.each(u_temp_artifacts.data, function (k, v) {
        if (v.level > 0) {
            litmus = true;
        }
    });
    if (false == litmus) {
        $('#suggestions').empty().append('<p>您必须至少启用一个神器才能接收优化建议方案。</p>');
        return
    }
    if (u_relics > 0) {
        if ('pct' == $('#ocd').val().substring(0, 3)) {
            u_step = parseInt($('#ocd').val().substring(3));
            upgrades.steps = [];
            window.setTimeout(optimizePct, 1);
        } else {
            u_step = parseInt($('#ocd').val());
            window.setTimeout(optimize, 1);
        }
    } else {
        renderSuggestions(u_temp_artifacts);
    }
}

function renderSuggestions(data) {
    if (winner_n != '') {
        $('#new_artifact').empty().append('<em>提示：未拥有的神器里这个对你影响最大（' + artifacts.data[winner_n].name + '）</em>');
    }
    winner_e = '';
    winner_e10 = '';
    winner_e100 = '';
    winner_e1000 = '';
    winner_n = '';
    winner_n_e = '';
    var suggestions = '';
    var litmus = false;
    $.each(upgrades, function (k, v) {
        litmus = true;
    });
    if (false == litmus) {
        $('#pudding').empty();
        $('#suggestions').empty().append('<p>您无法承担进行下一次升级的圣物消耗。 请积攒圣物或尝试降低购买指数或关闭四舍五入。</p>');
        $('#accept').empty().append('<button type="button" class="btn btn-danger" onclick="rejectSuggestions();">取消</button>');
        relics = 0;
        return;
    }
    $.each(artifacts.data, function (k, v) {
        if (k in upgrades) {
            suggestions += '<div class="card bg-dark border border-secondary ' + ($('#wolf').prop('checked') == true ? 'bg-dark' : '') + '">';
            suggestions += '<div class="card-header d-flex justify-content-between align-items-center" id="' + k + 'deetsh">';
            suggestions += '<span>';
            suggestions += '<span class="d-inline d-sm-none">' + v.name + '</span>';
            suggestions += '<span class="d-none d-sm-inline">' + v.name + '</span>';
            suggestions += ' <small>' + displayTruncated(v.level) + '&#x00A0;=>&#x00A0;' + displayTruncated(data.data[k].level) + '</small>';
            suggestions += '<span class="badge badge-' + v.color + ' ml-3">+' + upgrades[k] + '</span><br />';
            suggestions += '<small>花费 ' + displayTruncated(data.data[k].upgradeCost) + ' 圣物</small>';
            suggestions += '</span>';
            suggestions += '<button class="badge badge-secondary" type="button" data-toggle="collapse" data-target="#' + k + 'deets" aria-expanded="false" aria-controls="' + k + 'deets">&#x00A0;i&#x00A0;</button>';
            suggestions += '</div>';
            suggestions += '<div class="collapse" id="' + k + 'deets" aria-labelledby="' + k + 'deetsh" data-parent="#suggestions">';
            suggestions += '<div class="card-body">';
            suggestions += '<dl class="row">';
            suggestions += '<dt class="col-3 col-sm-6 text-right">Effect</dt>';
            suggestions += '<dd class="col-9 col-sm-6">' + displayEffect(v.current_effect, v.type) + ' => ' + displayEffect(data.data[k].current_effect, artifacts.data[k].type) + '</dd>';
            suggestions += '<dt class="col-3 col-sm-6 text-right">';
            suggestions += '<span class="d-block d-sm-none">AD</span>';
            suggestions += '<span class="d-none d-sm-block">Artifact Damage</span>';
            suggestions += '</dt>';
            suggestions += '<dd class="col-9 col-sm-6">' + displayPct(v.current_ad) + ' => ' + displayPct(data.data[k].current_ad) + '</dd>';
            suggestions += '</dl>';
            suggestions += '</div>';
            suggestions += '</div>';
            suggestions += '</div>';

        }
    });
    var alice = new Date();
    var curiouser = alice.getTime() - white_rabbit.getTime();
    $('#pudding').empty().append('共执行计算 ' + obfuscate + '次 在 ' + (curiouser / 1000).toFixed(3) + '秒内 (' + ((obfuscate / curiouser) * 1000).toFixed(3) + '/s)');
    $('#suggestions').empty().append(suggestions);
    $('#accept2').show();
    $('#accept').empty().append('<button type="button" class="btn btn-primary" onclick="acceptSuggestions();">完成</button><button type="button" class="btn btn-danger" onclick="rejectSuggestions();">取消</button>');
}

function renderPctSuggestions(data) {
    if (winner_n != '') {
        $('#new_artifact').empty().append('<em>提示：未拥有的神器里这个对你影响最大（' + artifacts.data[winner_n].name + '）</em>');
    }
    winner_e = '';
    winner_e10 = '';
    winner_e100 = '';
    winner_e1000 = '';
    winner_n = '';
    winner_n_e = '';
    var suggestions = '<ol>';
    if (0 == upgrades.steps.length) {
        $('#pudding').empty();
        $('#suggestions').empty().append('<p>您无法承担进行下一次升级的圣物消耗。 请积攒圣物或尝试降低购买指数或关闭四舍五入。</p>');
        $('#accept').empty().append('<button type="button" class="btn btn-danger" onclick="rejectSuggestions();">取消</button>');
        u_relics = 0;
        return;
    }
    $.each(upgrades.steps, function (k, v) {
        suggestions += '<li class="mb-2">';
        suggestions += '<span>' + artifacts.data[v.k].name + '</span>';
        suggestions += '<sup class="text-secondary">[' + artifacts.data[v.k].sort_order + ']</sup>';
        suggestions += '<span class="badge badge-' + artifacts.data[v.k].color + ' ml-3">+' + displayTruncated(v.levels) + '</span><br />';
        suggestions += '<small>花费： ' + displayTruncated(v.cost) + ' // 剩余： ' + displayTruncated(v.remaining) + '</small>';
        suggestions += '</li>';
    });
    suggestions += '</ol>';
    var alice = new Date();
    var curiouser = alice.getTime() - white_rabbit.getTime();
    $('#pudding').empty().append('共执行计算 ' + obfuscate + '次 在 ' + (curiouser / 1000).toFixed(3) + '秒内 (' + ((obfuscate / curiouser) * 1000).toFixed(3) + '/s)');
    $('#suggestions').empty().append(suggestions);
    $('#accept2').show();
    $('#accept').empty().append('<button type="button" class="btn btn-primary" onclick="acceptPctSuggestions();">完成</button><button type="button" class="btn btn-danger" onclick="rejectSuggestions();">取消</button>');
}

function acceptPctSuggestions() {
    gtag('event', 'Upgrades', {
        'event_category': 'Upgrades',
        'event_action': 'Accept',
        'event_label': 'Artifacts Pct',
    });
    $.each(upgrades.steps, function (k, v) {
        artifacts.data[v.k].level += v.levels;
    });
    artifacts.totalAD = calculateTotalAD(artifacts.data, true);
    $('#new_artifact').empty();
    $('#accept').empty();
    $('#accept2').hide();
    $('#suggestions').empty();
    $('#relics').val('');
    $('#relics_decimal').val('');
    $('#relicsuggs').hide();
    $('#relicreccs').show();
    adjustWeights(true);
}

function acceptSuggestions() {
    gtag('event', 'Upgrades', {
        'event_category': 'Upgrades',
        'event_action': 'Accept',
        'event_label': 'Artifacts',
    });
    $.each(upgrades, function (k, v) {
        artifacts.data[k].level += v;
    });
    artifacts.totalAD = calculateTotalAD(artifacts.data, true);
    $('#new_artifact').empty();
    $('#accept').empty();
    $('#accept2').hide();
    $('#suggestions').empty();
    $('#relics').val('');
    $('#relics_decimal').val('');
    $('#relicsuggs').hide();
    $('#relicreccs').show();
    adjustWeights(true);
}

function rejectSuggestions() {
    $('#new_artifact').empty();
    $('#accept').empty();
    $('#accept2').hide();
    $('#suggestions').empty();
    $('#relics').val('');
    $('#relics_decimal').val('');
    $('#relicsuggs').hide();
    $('#relicreccs').show();
    calculateAll(artifacts, true);
}

function skillEff(k, v) {
    var current_effect = false;
    var current_effect2 = false;
    var current_effect3 = false;
    if (0 < v.level) {
        current_effect = v.levels[v.level].bonus;
    }
    if (-1 != v.bonus2) {
        current_effect2 = v.level > 0 ? v.levels[v.level].bonus2 : 'X';
    }
    if (-1 != v.bonus3) {
        current_effect3 = v.level > 0 ? v.levels[v.level].bonus3 : 'X';
    }
    skills.data[k].current_effect = current_effect;
    skills.data[k].current_effect2 = current_effect2;
    skills.data[k].current_effect3 = current_effect3;
    var running_eff = 1;
    if (v.max < v.level) {
        v.level = v.max;
        skills.data[k].level = v.max;
        $('#skill' + k).val(v.max);
    }
    var active = 'online' == $('#active').val();
    if (v.max > v.level) {
        if (false === current_effect) {
            current_effect = 0;
        }
        skills.data[k].cost = v.levels[v.level + 1].cost;
        var lvl = v.level + 1;
        var totalCost = 0;
        while (lvl > 0) {
            totalCost += v.levels[lvl--].cost;
        }
        var next_effect = v.levels[v.level + 1].bonus;
        if ('aaw' == k && 0 < v.level) {
            next_effect = Math.pow(next_effect, v.levels[v.level + 1].bonus3);
            current_effect = Math.pow(current_effect, v.levels[v.level].bonus3);
        }
        var effect_diff = Math.abs(next_effect) / (0 < v.level && 0 != current_effect && 'X' != current_effect ? Math.abs(current_effect) : Math.abs(next_effect / 2));
        var effect_eff = Math.pow(effect_diff, (0 == v.rating1 ? .00001 : v.rating1));
        running_eff *= effect_eff;
        if (false !== current_effect2) {
            var next_effect2 = v.levels[v.level + 1].bonus2;
            if (0 != next_effect2) {
                var effect_diff2 = Math.abs(next_effect2) / (0 < v.level && 0 != current_effect2 && 'X' != current_effect2 ? Math.abs(current_effect2) : Math.abs(next_effect2 / 2));
                var effect_eff2 = Math.pow(effect_diff2, (0 == v.rating2 ? .00001 : v.rating2));
                if ('cs' == k) {
                    running_eff /= effect_eff2;
                } else {
                    running_eff *= effect_eff2;
                }
            }
        }
        if (false !== current_effect3) {
            var next_effect3 = v.levels[v.level + 1].bonus3;
            if (0 != next_effect3) {
                var effect_diff3 = Math.abs(next_effect3) / (0 < v.level && 0 != current_effect3 && 'X' != current_effect3 ? Math.abs(current_effect3) : Math.abs(next_effect3 / 2));
                var effect_eff3 = Math.pow(effect_diff3, (0 == v.rating3 ? .00001 : v.rating3));
                running_eff *= effect_eff3;
            }
        }
        var effDec = Decimal(running_eff);
        var eff = effDec.pow(1 / v.levels[v.level + 1].cost).sub(1).toNumber();
        skills.data[k].efficiency = eff;
    }
}

function oldEff(data, k, v) {
    var current_ad = v.level * v.ad;
    var current_effect = 1 + v.effect * Math.pow(v.level, Math.pow((1 + (v.cexpo - 1) * Math.min(v.grate * v.level, v.gmax)), v.gexpo));
    switch (v.dime) {
        case 0:
            break;
        case 1:
            if (0 < artifacts.data.tmg.level) {
                current_effect *= 10;
            }
            break;
        case 2:
            if (0 < artifacts.data.ttof.level) {
                current_effect *= 10;
            }
            break;
    }
    data.data[k].current_ad = current_ad;
    data.data[k].current_effect = current_effect;
    if (v.max == -1 || v.max > v.level) {
        var cost = Math.pow(v.level + 1, v.cexpo) * v.ccoef;
        data.data[k].cost = cost;
        data.data[k].displayCost = displayTruncated(cost);
        data.data[k].efficiency = calculateArtifactEfficiency(v, cost, 1, current_ad, current_effect, data.totalAD);
        var int10 = calculateArtifactEfficiencyInterval(v, 10);
        var cost10 = calculateArtifactEfficiencyCost(v, int10);
        data.data[k].efficiency10_int = int10;
        data.data[k].efficiency10_cost = cost10;
        data.data[k].efficiency10 = calculateArtifactEfficiency(v, cost10, int10, current_ad, current_effect, data.totalAD);
        var int100 = calculateArtifactEfficiencyInterval(v, 100);
        var cost100 = calculateArtifactEfficiencyCost(v, int100);
        data.data[k].efficiency100_int = int100;
        data.data[k].efficiency100_cost = cost100;
        data.data[k].efficiency100 = calculateArtifactEfficiency(v, cost100, int100, current_ad, current_effect, data.totalAD);
        var int1000 = calculateArtifactEfficiencyInterval(v, 1000);
        var cost1000 = calculateArtifactEfficiencyCost(v, int1000);
        data.data[k].efficiency1000_int = int1000;
        data.data[k].efficiency1000_cost = cost1000;
        data.data[k].efficiency1000 = calculateArtifactEfficiency(v, cost1000, int1000, current_ad, current_effect, data.totalAD);
    }
    return (data);
}

function calculateArtifactEfficiencyInterval(v, levels) {
    var squad = (v.level + levels) % levels;
    if (0 != squad) {
        levels = levels - squad;
    }
    if (0 < v.max && v.level + levels > v.max) {
        levels = v.max - v.level;
    }
    return (levels);
}

function calculateArtifactEfficiencyCost(v, levels) {
    obfuscate++;
    var cost = (v.ccoef / (v.cexpo + 1)) * Math.pow(v.level + levels, v.cexpo + 1) - (v.ccoef / (v.cexpo + 1)) * Math.pow(v.level, v.cexpo + 1);
    return (cost);
}

function calculateArtifactEfficiency(v, cost, lvlChange, current_ad, current_effect, totalAD) {
    obfuscate++;
    var next_effect = 1 + v.effect * Math.pow(v.level + lvlChange, Math.pow((1 + (v.cexpo - 1) * Math.min(v.grate * (v.level + lvlChange), v.gmax)), v.gexpo));
    switch (v.dime) {
        case 0:
            break;
        case 1:
            if (0 < artifacts.data.tmg.level) {
                next_effect *= 10;
            }
            break;
        case 2:
            if (0 < artifacts.data.ttof.level) {
                next_effect *= 10;
            }
            break;
    }
    var effect_diff = Math.abs(next_effect) / Math.abs(current_effect);
    var effect_eff = Math.pow(effect_diff, v.rating);
    var ad_change = (((v.level + lvlChange) * v.ad) - current_ad);
    var ad_eff = 1 + (ad_change / totalAD);
    var eff = Math.abs(((effect_eff * ad_eff) - 1) / cost);
    return (eff);
}

function newEff(data, k, v, avglvl, cost, remainingArtifacts) {
    data.data[k].current_ad = '';
    data.data[k].current_effect = '';
    v.level = 1;
    var j = (v.max == -1 || v.max > avglvl ? avglvl : v.max);
    cost += calculateArtifactEfficiencyCost(v, j);
    v.level = 0;
    if (v.max == -1 || v.max > avglvl) {
        var next_effect = 1 + v.effect * Math.pow(avglvl, Math.pow((1 + (v.cexpo - 1) * Math.min(v.grate * avglvl, v.gmax)), v.gexpo));
    } else {
        var next_effect = 1 + v.effect * Math.pow(v.max, Math.pow((1 + (v.cexpo - 1) * Math.min(v.grate * v.max, v.gmax)), v.gexpo));
    }
    var effect_eff = Math.pow(Math.abs(next_effect), v.rating);
    var ad_eff = 1 + ((avglvl * v.ad) / data.totalAD);
    var eff = Math.abs(((effect_eff * ad_eff) - 1) / cost / remainingArtifacts);
    data.data[k].efficiency = eff;
    return (data)
}

function calculateTotalAD(data, update) {
    var total = 0;
    $.each(data, function (k, v) {
        obfuscate++;
        total += v.level * v.ad;
    });
    if (true == update) {
        $('#adsanity').text(displayPct(total * ("" != artifacts.data.hsw.current_effect ? artifacts.data.hsw.current_effect : 1)));
    }
    return (total);
}

function calculateSkillTotals() {
    skills.totals.SP = 0;
    skills.totals.red = 0;
    skills.totals.yellow = 0;
    skills.totals.blue = 0;
    skills.totals.green = 0;
    $.each(skills.data, function (k, v) {
        if (v.level > v.max) {
            v.level = v.max;
            skills.data[k].level = v.max;
            $('#skill' + k).val(v.max);
        }
        if (v.level > 0) {
            var lvl = 1;
            while (lvl <= v.level) {
                var stats = v.levels[lvl];
                skills.totals.SP += stats.cost;
                skills.totals[v.branch] += stats.cost;
                lvl++;
            }
        }
    });
    $('#totalSP').text(skills.totals.SP);
    $('#totalSPred').text(skills.totals.red);
    $('#totalSPyellow').text(skills.totals.yellow);
    $('#totalSPblue').text(skills.totals.blue);
    $('#totalSPgreen').text(skills.totals.green);
}

function calculate(data, k, regenerate, pinch) {
    obfuscate += 1;
    var next_artifact = countArtifacts(artifacts.data) + 1;
    var next_artifact_cost = artifact_costs[next_artifact];
    var average_level = determineAverage(artifacts.data);
    var v = data.data[k];
    data.data[k].efficiency = '';
    data.data[k].cost = '';
    data.data[k].displayCost = '';
    if (v.level > 0 && v.active == 1) {
        var prior_ad = v.current_ad;
        data = oldEff(data, k, v);
        var new_ad = data.data[k].current_ad;
    } else if (v.level == 0 && next_artifact_cost != -1 && v.active == 1 && true === pinch) {
        data = newEff(data, k, v, average_level, next_artifact_cost, Object.keys(artifact_costs).length - 3 - next_artifact);
    } else {
        data.data[k].current_ad = '';
        data.data[k].current_effect = '';
    }
    determineArtifactWinner(data, regenerate, next_artifact_cost, pinch);
    data.totalAD = calculateTotalAD(data.data, regenerate);
    return (data);
}

function determineArtifactWinner(data, regenerate, next_artifact_cost, pinch) {
    winner_e = '';
    winner_e10 = '';
    winner_e100 = '';
    winner_e1000 = '';
    var temp_winner_n = '';
    var temp_winner_n_e = '';
    var temp_winner_value = 0;
    winner_value = 0;
    winner_value10 = 0;
    winner_value100 = 0;
    winner_value1000 = 0;
    $.each(data.data, function (k, v) {
        obfuscate++;
        if (v.efficiency > winner_value) {
            if (v.level > 0 && v.active == 1 && (-1 == v.max || v.max > v.level)) {
                winner_e = k;
                winner_value = v.efficiency;
            } else if (v.level == 0 && next_artifact_cost != -1 && v.active == 1 && true === pinch) {
                if (data.data[k].efficiency > temp_winner_value && ('' == temp_winner_n_e || temp_winner_n_e < data.data[k].rating)) {
                    temp_winner_n = k;
                    temp_winner_n_e = data.data[k].rating;
                    temp_winner_value = data.data[k].efficiency;
                }
            }
        }
        obfuscate++;
        if (v.efficiency10 > winner_value10) {
            if (v.level > 0 && v.active == 1 && (-1 == v.max || v.max > v.level)) {
                winner_e10 = k;
                winner_value10 = v.efficiency10;
            }
        }
        obfuscate++;
        if (v.efficiency100 > winner_value100) {
            if (v.level > 0 && v.active == 1 && (-1 == v.max || v.max > v.level)) {
                winner_e100 = k;
                winner_value100 = v.efficiency100;
            }
        }
        obfuscate++;
        if (v.efficiency1000 > winner_value1000) {
            if (v.level > 0 && v.active == 1 && (-1 == v.max || v.max > v.level)) {
                winner_e1000 = k;
                winner_value1000 = v.efficiency1000;
            }
        }
    });
    if (true === regenerate) {
        regenerateArtifacts();
        if ('' != temp_winner_n && data.data[temp_winner_n].efficiency >= winner_value && "1" == data.data[temp_winner_n].active) {
            winner_n = temp_winner_n;
        } else {
            winner_n = '';
        }
    }

}

function determineSkillWinner(prevWinners) {
    winner_svalue = 0;
    var winner = '';
    $.each(skills.data, function (k, v) {
        if (-1 != prevWinners.indexOf(k)) {
            return true;
        }
        if (v.efficiency > winner_svalue && v.max > v.level) {
            if (skills.totals[v.branch] >= tiers[v.tier] && (-1 == v.prereq || 0 < skills.data[v.prereq].level)) {
                winner = k;
                winner_svalue = v.efficiency;
            } else if (skills.totals[v.branch] < tiers[v.tier] && -1 == skills.data[v.prereq].prereq && -1 == prevWinners.indexOf(v.prereq)) {
                winner = v.prereq;
                winner_svalue = v.efficiency;
            } else if (skills.totals[v.branch] >= tiers[v.tier] && -1 == skills.data[skills.data[v.prereq].prereq].prereq) {
                if (0 < skills.data[skills.data[v.prereq].prereq].level && -1 == prevWinners.indexOf(v.prereq)) {
                    winner = v.prereq;
                    winner_svalue = v.efficiency;
                } else if (-1 == prevWinners.indexOf(skills.data[v.prereq].prereq)) {
                    winner = skills.data[v.prereq].prereq;
                    winner_svalue = v.efficiency;
                }
            } else if (skills.totals[v.branch] >= tiers[v.tier] && -1 == skills.data[skills.data[skills.data[v.prereq].prereq].prereq].prereq) {
                if (0 < skills.data[skills.data[skills.data[v.prereq].prereq].prereq].level) {
                    if (0 < skills.data[skills.data[v.prereq].prereq].level && -1 == prevWinners.indexOf(v.prereq)) {
                        winner = v.prereq;
                        winner_svalue = v.efficiency;
                    } else if (-1 == prevWinners.indexOf(skills.data[v.prereq].prereq)) {
                        winner = skills.data[v.prereq].prereq;
                        winner_svalue = v.efficiency;
                    }
                } else if (-1 == prevWinners.indexOf(skills.data[skills.data[v.prereq].prereq].prereq)) {
                    winner = skills.data[skills.data[v.prereq].prereq].prereq;
                    winner_svalue = v.efficiency;
                }
            }
        }
    });
    return (winner);
}

function calculateAllSkills() {
    winner_s1 = '';
    winner_s2 = '';
    winner_s3 = '';
    winner_s4 = '';
    winner_s5 = '';
    var prevWinners = [];
    $.each(skills.data, function (k, v) {
        skills.data[k].efficiency = 0;
        skills.data[k].cost = '';
        if (v.active == 1) {
            skillEff(k, v);
        } else {
            skills.data[k].current_effect = '';
            skills.data[k].current_effect2 = '';
            skills.data[k].current_effect3 = '';
        }
    });
    winner_s1 = determineSkillWinner(prevWinners);
    prevWinners.push(winner_s1);
    winner_s2 = determineSkillWinner(prevWinners);
    prevWinners.push(winner_s2);
    winner_s3 = determineSkillWinner(prevWinners);
    prevWinners.push(winner_s3);
    winner_s4 = determineSkillWinner(prevWinners);
    prevWinners.push(winner_s4);
    winner_s5 = determineSkillWinner(prevWinners);
    calculateSkillTotals();
    regenerateSkills();
    var next_skill = '';
    if ('' != winner_s1) {
        next_skill += '<button type="button" class="btn btn-primary mb-2 mr-2 col-12 col-md-6 col-xl-4" onclick="acceptSkill(\'' + winner_s1 + '\')">#1: ' + skills.data[winner_s1].name + ' ' + skills.data[winner_s1].nickname + ' (' + skills.data[winner_s1].cost + ' SP - ' + (skills.data[winner_s1].efficiency / skills.data[winner_s1].cost * 100).toFixed(2) + '%) <span class="badge ml-2 badge-pill badge-' + ('info' == skills.data[winner_s1].color ? 'success' : skills.data[winner_s1].color) + '">' + skills.data[winner_s1].efficiency.toExponential(3) + '</span>';
    }
    if ('' != winner_s2) {
        next_skill += '<button type="button" class="btn btn-primary mb-2 mr-2 col-12 col-md-6 col-xl-4" onclick="acceptSkill(\'' + winner_s2 + '\')">#2: ' + skills.data[winner_s2].name + ' ' + skills.data[winner_s2].nickname + ' (' + skills.data[winner_s2].cost + ' SP - ' + (skills.data[winner_s2].efficiency / skills.data[winner_s2].cost * 100).toFixed(2) + '%) <span class="badge ml-2 badge-pill badge-' + ('info' == skills.data[winner_s2].color ? 'success' : skills.data[winner_s2].color) + '">' + skills.data[winner_s2].efficiency.toExponential(3) + '</span>';
    }
    if ('' != winner_s3) {
        next_skill += '<button type="button" class="btn btn-primary mb-2 mr-2 col-12 col-md-6 col-xl-4" onclick="acceptSkill(\'' + winner_s3 + '\')">#3: ' + skills.data[winner_s3].name + ' ' + skills.data[winner_s3].nickname + ' (' + skills.data[winner_s3].cost + ' SP - ' + (skills.data[winner_s3].efficiency / skills.data[winner_s3].cost * 100).toFixed(2) + '%) <span class="badge ml-2 badge-pill badge-' + ('info' == skills.data[winner_s3].color ? 'success' : skills.data[winner_s3].color) + '">' + skills.data[winner_s3].efficiency.toExponential(3) + '</span></button>';
    }
    if ('' != winner_s4) {
        next_skill += '<button type="button" class="btn btn-primary mb-2 mr-2 col-12 col-md-6 col-xl-4" onclick="acceptSkill(\'' + winner_s4 + '\')">#4: ' + skills.data[winner_s4].name + ' ' + skills.data[winner_s4].nickname + ' (' + skills.data[winner_s4].cost + ' SP - ' + (skills.data[winner_s4].efficiency / skills.data[winner_s4].cost * 100).toFixed(2) + '%) <span class="badge ml-2 badge-pill badge-' + ('info' == skills.data[winner_s4].color ? 'success' : skills.data[winner_s4].color) + '">' + skills.data[winner_s4].efficiency.toExponential(3) + '</span></button>';
    }
    if ('' != winner_s5) {
        next_skill += '<button type="button" class="btn btn-primary mb-2 mr-2 col-12 col-md-6 col-xl-4" onclick="acceptSkill(\'' + winner_s5 + '\')">#5: ' + skills.data[winner_s5].name + ' ' + skills.data[winner_s5].nickname + ' (' + skills.data[winner_s5].cost + ' SP - ' + (skills.data[winner_s5].efficiency / skills.data[winner_s5].cost * 100).toFixed(2) + '%) <span class="badge ml-2 badge-pill badge-' + ('info' == skills.data[winner_s5].color ? 'success' : skills.data[winner_s5].color) + '">' + skills.data[winner_s5].efficiency.toExponential(3) + '</span></button>';
    }
    next_skill += '';
    $('#nextskill').empty().append(next_skill);
}

function acceptSkill(skill) {
    gtag('event', 'Upgrades', {
        'event_category': 'Upgrades',
        'event_action': 'Accept',
        'event_label': 'SP',
    });
    comeUndone = skill;
    skills.data[skill].level++;
    calculateSkillTotals();
    adjustWeights(false);
}

function undoSkill() {
    gtag('event', 'Upgrades', {
        'event_category': 'Upgrades',
        'event_action': 'Undo',
        'event_label': 'SP',
    });
    if ('' != comeUndone) {
        skills.data[comeUndone].level--;
        calculateSkillTotals();
        adjustWeights(false);
        comeUndone = '';
    }
}

function calculateAll(data, regenerate) {
    winner_e = '';
    var temp_winner_n = '';
    var temp_winner_n_e = '';
    var temp_winner_value = 0;
    winner_value = 0;
    var next_artifact = countArtifacts(artifacts.data) + 1;
    var next_artifact_cost = artifact_costs[next_artifact];
    var average_level = determineAverage(artifacts.data);
    $.each(data.data, function (k, v) {
        data.data[k].cost = '';
        data.data[k].displayCost = '';
        if (v.level > 0 && v.active == 1) {
            data = oldEff(data, k, v);
        } else if (v.level == 0 && next_artifact_cost != -1 && v.active == 1) {
            data = newEff(data, k, v, average_level, next_artifact_cost, Object.keys(artifact_costs).length - 1 - next_artifact);
        } else {
            data.data[k].current_ad = '';
            data.data[k].current_effect = '';
        }
    });
    determineArtifactWinner(data, regenerate, next_artifact_cost, true);
    data.totalAD = calculateTotalAD(data.data, regenerate);
    return (data)
}

function displayPct(value) {
    value = displayTruncated(value * 100);
    return (value + '%');
}

function displayTruncated(value) {
    if (value > 999999999999999) {
        value = value.toExponential(2);
        value = value.replace(/\+/, '');
    } else {
        if (value > 999999999999) {
            value = (value / 1000000000000).toFixed(2).replace(/\.?0+$/, '');
            value += 'T';
        } else if (value > 999999999) {
            value = (value / 1000000000).toFixed(2).replace(/\.?0+$/, '');
            value += 'B';
        } else if (value > 999999) {
            value = (value / 1000000).toFixed(2).replace(/\.?0+$/, '');
            value += 'M';
        } else if (value > 999) {
            value = (value / 1000).toFixed(2).replace(/\.?0+$/, '');
            value += 'K';
        } else if (isNaN(value)) {
            value = value.toFixed(2).replace(/\.?0+$/, '');
        }
    }
    return (value);
}

function displayEffect(value, type) {
    switch (type) {
        case 'multiply':
            return 'x' + displayTruncated(value);
            break;
        case 'add':
            if (false != value) {
                value = value - 1
            }
            if (value > 0) {
                return '+' + displayTruncated(value);
            } else {
                return displayTruncated(value);
            }
            break;
        case 'add_skill':
            if (value > 0) {
                return '+' + displayTruncated(value);
            } else {
                return displayTruncated(value);
            }
            break;
        case 'multiply_pct':
            return 'x' + displayPct(value);
            break;
        case 'pct':
            value = value - 1
            if (value > 0) {
                return '+' + displayPct(value);
            } else {
                return displayPct(value);
            }
            break;
        case 'pct_pos':
            if (value > 0) {
                return '+' + displayPct(value);
            } else {
                return displayPct(value);
            }
            break;
    }
}

function storageAvailable(type) {
    try {
        var storage = window[type],
            x = '__storage_test__';
        storage.setItem(x, x);
        storage.removeItem(x);
        return true;
    } catch (e) {
        return e instanceof DOMException && (
                // everything except Firefox
            e.code === 22 ||
            // Firefox
            e.code === 1014 ||
            // test name field too, because code might not be present
            // everything except Firefox
            e.name === 'QuotaExceededError' ||
            // Firefox
            e.name === 'NS_ERROR_DOM_QUOTA_REACHED') &&
            // acknowledge QuotaExceededError only if there's something already stored
            storage.length !== 0;
    }
}

if (storageAvailable('localStorage')) {
    var localArtifacts = JSON.parse(window.localStorage.getItem('artifacts'));
    if (null != localArtifacts && 'undefined' == typeof localArtifacts.data) {
        localArtifacts.data = jQuery.extend(true, {}, localArtifacts);
    }
    if (null != localArtifacts && 'undefined' != typeof localArtifacts.data) {
        $.each(localArtifacts.data, function (k, v) {
            if (undefined != artifacts.data[k]) {
                artifacts.data[k].level = v.level;
                artifacts.data[k].active = v.active;
            }
        });
    }
    artifacts.totalAD = calculateTotalAD(artifacts.data, true);
    var localSkills = JSON.parse(window.localStorage.getItem('skills'));
    if (null != localSkills && 'undefined' == typeof localSkills.data) {
        localSkills.data = jQuery.extend(true, {}, localSkills);
    }
    if (null != localSkills && 'undefined' != typeof localSkills.data) {
        $.each(localSkills.data, function (k, v) {
            if (undefined != skills.data[k]) {
                skills.data[k].level = (v.max < v.level ? v.max : v.level);
                skills.data[k].active = v.active;
            }
        });
    }
    calculateSkillTotals();
    $('#build').val(window.localStorage.getItem('build'));
    $('#hero').val(window.localStorage.getItem('hero'));
    $('#gold').val(window.localStorage.getItem('gold'));
    $('#active').val(window.localStorage.getItem('active'));
    $('#relic_factor').val(window.localStorage.getItem('relic_factor'));
    $('#spend').val(window.localStorage.getItem('spend'));
    $('#spend_decimal').val(window.localStorage.getItem('spend_decimal'));
    var ocd = window.localStorage.getItem('ocd');
    if (ocd) {
        if ('pct' != ocd.substring(0, 3)) {
            if (1000 < parseInt(ocd)) {
                ocd = 1000;
            }
        }
    } else {
        ocd = 1;
    }
    $('#ocd').val(ocd.toString());
    if (window.localStorage.getItem('splash') == "1") {
        $('#wet').prop('checked', true);
        $('#dry').prop('checked', false);
    } else {
        $('#wet').prop('checked', false);
        $('#dry').prop('checked', true);
    }
    toggleSplash(false);
}

function storeData() {
    window.localStorage.setItem('artifacts', JSON.stringify(artifacts));
    window.localStorage.setItem('skills', JSON.stringify(skills));
    window.localStorage.setItem('build', $('#build').val());
    window.localStorage.setItem('hero', $('#hero').val());
    window.localStorage.setItem('gold', $('#gold').val());
    window.localStorage.setItem('active', $('#active').val());
    window.localStorage.setItem('relic_factor', $('#relic_factor').val());
    window.localStorage.setItem('spend', $('#spend').val());
    window.localStorage.setItem('spend_decimal', $('#spend_decimal').val());
    window.localStorage.setItem('ocd', $('#ocd').val());
    window.localStorage.setItem('splash', ($('#wet').prop('checked') == true ? 1 : 0));
}

$('input[type="tel"]').on('focus', function () {
    $(this).data('fontSize', $(this).css('font-size')).css('font-size', '16px');
}).on('blur', function () {
    $(this).css('font-size', $(this).data('fontSize'));
});
$('input[type="number"]').on('focus', function () {
    $(this).data('fontSize', $(this).css('font-size')).css('font-size', '16px');
}).on('blur', function () {
    $(this).css('font-size', $(this).data('fontSize'));
});

function avoidSci(x) {
    if (Math.abs(x) < 1.0) {
        var e = parseInt(x.toString().split('e-')[1]);
        if (e) {
            x *= Math.pow(10, e - 1);
            x = '0.' + (new Array(e)).join('0') + x.toString().substring(2);
        }
    } else {
        var e = parseInt(x.toString().split('+')[1]);
        if (e > 20) {
            e -= 20;
            x /= Math.pow(10, e);
            x += (new Array(e + 1)).join('0');
        }
    }
    return x;
}

function exportData() {
    $('#export_wrap').hide();
    $('#import_wrap').hide();
    var ex = '';
    ex += $('#build').val() + '=';
    ex += $('#hero').val() + '=';
    ex += $('#gold').val() + '=';
    ex += $('#active').val() + '=';
    ex += ($('#spend').val() == '' ? 75 : $('#spend').val()) + '=';
    ex += ($('#spend_decimal').val() == '' ? 0 : $('#spend_decimal').val()) + '=';
    ex += ($('#wet').prop('checked') == true ? 1 : 0) + '=';
    ex += $('#relic_factor').val() + '=';
    ex += $('#ocd').val() + '=';
    $.each(artifacts.data, function (k, v) {
        ex += k + '_';
        ex += v.active + '_';
        ex += (999999999999999 < v.level ? displayTruncated(v.level) : avoidSci(v.level)) + '|';
    });
    ex = ex.slice(0, -1);
    ex += '=';
    $.each(skills.data, function (k, v) {
        ex += k + '_';
        ex += v.active + '_';
        ex += v.level + '|';
    });
    ex = ex.slice(0, -1);
    $('#export').empty().text(ex);
    $('#export_wrap').show();
}

function startImport() {
    $('#export_wrap').hide();
    $('#import_wrap').hide();
    $('#import').empty();
    $('#import_wrap').show();
}

function importData() {
    var im = ($('#import').val().trim().split('='));
    $('#build').val(im[0]);
    $('#hero').val(im[1]);
    $('#gold').val(im[2]);
    $('#active').val(im[3]);
    $('#spend').val(im[4]);
    $('#spend_decimal').val(im[5]);
    if (im[6] == "1") {
        $('#wet').prop('checked', true);
        $('#dry').prop('checked', false);
    } else {
        $('#wet').prop('checked', false);
        $('#dry').prop('checked', true);
    }
    toggleSplash(false);
    $('#relic_factor').val(im[7]);
    var ocd = im[8];
    if ('pct' != ocd.substring(0, 3)) {
        if (1000 < parseInt(ocd)) {
            ocd = 1000;
        }
    }
    $('#ocd').val(ocd.toString());
    var ima = im[9].split('|');
    $.each(ima, function (k, v) {
        var imaa = v.split('_');
        artifacts.data[imaa[0]].active = parseInt(imaa[1]);
        artifacts.data[imaa[0]].level = parseFloat(imaa[2]);
    });
    var ims = im[10].split('|');
    $.each(ims, function (k, v) {
        var imss = v.split('_');
        skills.data[imss[0]].active = parseInt(imss[1]);
        skills.data[imss[0]].level = parseInt(imss[2]);
    });
    $('#export_wrap').hide();
    $('#import_wrap').hide();
    generateArtifacts();
    generateSkills();
    adjustWeights(true);
}

$('#export_wrap').hide();
$('#import_wrap').hide();
$('#relicsuggs').hide();
generateArtifacts();
generateSkills();
if (false == halp) {
    adjustWeights(true);
}
