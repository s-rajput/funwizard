import * as c from '@/api/fun-data'

describe('currency model', () => {
    describe('convert function', () => {
        it('should convert fare to foreign fare based on the bsr if a fare is passed in', () => {
            const output = c.authenticate('test','tt','')
            expect(output.then(function(result) { })).toEqual(140)
        })
 
    })
})
