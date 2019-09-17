import { BaseVM } from '@common/BaseModel';
import { Guid } from '@app/common/guid';

export class User extends BaseVM{
    public userName: string;
    public passwordHash: string;
    public lastLoginDateTime: number;
    public createDateTime: number;
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
        this.statusVal=4;
        this.token="";        
    }
}

export class UserLoginVM{
    public UserName: string;
    public PasswordHash: string;
}

export class UserListVM extends BaseVM{
    public userName: string;
    public isAdmin: boolean;
    public displayName: string;
}

export class UserRegisterVM{
    public userName: string;
    public passwordHash: string;
  
    public isAdmin: boolean;

    public displayName: string;
    public about: string;

    public status: number;

    constructor(){
    }
}

export class UserUpdateVM extends BaseVM{
    public userName: string;
    public passwordHash: string;
    public oldPassword: string;
    public isAdmin: boolean;
    public isBanned: boolean;

    public displayName: string;
    public about: string;

    public status: number;

    constructor(){
        super();
    }
}

