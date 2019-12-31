import { AddVM } from '@app/shared/models/BaseModel';

export class UserRegisterVM extends AddVM{
    public userName: string;
    public passwordHash: string;
  
    public isAdmin: boolean;

    public displayName: string;
    public about: string;

    public status: number;

    constructor(){
        super();
    }
}
