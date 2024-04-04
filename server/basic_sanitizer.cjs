// Basic japanese script recognition pattern
patterns = {
    "jaGeneral": /[ぁ-んァ-ン一-龠ー、。、！？]+/gu,
    "jaExtended": /[0-9ｦ-ﾟァ-ヶぁ-ゞＡ-ｚ０-９ｧ-ﾝﾞﾟぁ-んァ-ン一-龯ー、。、！？：／\]\[:?"〈〉,《》【】()「」『』]+/gu,
    "enGeneral": /[A-Za-z0-9'’“”.,!?()\s@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?`~]+/gu,
}

const sanitizeInput = (data, pat) => {
    
    const matches = data.match(patterns[pat]);

    let separatedMatches = [];
    if (matches) {
        
        separatedMatches = matches.reduce((accumulator, currentMatch) => {
            return accumulator + currentMatch + '\n';
        }, '').split("\n");

        separatedMatches.forEach(element => {
           console.log(element);
        });

        return separatedMatches[0];
    }

    return "Could not parse\n" + data;
}

module.exports = sanitizeInput;