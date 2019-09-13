import { BaseVM } from '@common/BaseModel';
import { Guid } from '@app/common/guid';

export class User extends BaseVM{
    public userName: string;
    public passwordHash: string;
    public lastLoginDateTime: number;
  
    public isAdmin: boolean;
    public isBanned: boolean;

    public displayName: string;
    public about: string;

    public statusVal: number;

    public token: string;

    constructor(){
        super();
        this.id = Guid.newGuid();
        this.userName= "";
        this.passwordHash="";
        this.displayName="";
        this.about="";
        this.isAdmin=false;
        this.isBanned=false;
        this.lastLoginDateTime=Date.now();
        this.statusVal=4;
        this.token="";
    }
}

export class UserLoginVM{
    public UserName: string;
    public PasswordHash: string;
}