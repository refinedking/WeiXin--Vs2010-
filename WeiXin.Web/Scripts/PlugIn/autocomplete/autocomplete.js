function autoComp(id, url, action) {
    $("#" + id + "").autocomplete("" + url + "?action=" + action + "", { minChars: 1, max: 100, autoFill: true, mustMatch: true, matchContains: true, selectFirst: true, matchCase: true, cacheLength: 20, autoComplete: 20 });
}