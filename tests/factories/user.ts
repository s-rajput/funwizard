import { User } from '@/models/user'

export function create (): User {
    return {
        FirstName: 'test',
        LastName: 'test',
        Name: 'Admin',
        Email: 'Tester@c.com',
        Id: '1'
    }
}
