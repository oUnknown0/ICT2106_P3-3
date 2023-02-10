export default class U {
    static checkSubset = (subset, set) => {
        return subset.every((value) => {
            return set.includes(value);
        });
    }
}