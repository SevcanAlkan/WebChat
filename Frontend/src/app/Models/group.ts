import { BaseVM, AddVM } from '@app/common/baseModel';

export class GroupVM extends BaseVM{
    public name: string;
    public description: string; 
    public isMain: boolean;
    public isPrivate: boolean;
}

export class GroupAddVM extends AddVM{
    public name: string;
    public description: string; 
    public isMain: boolean;
    public isPrivate: boolean;
    public users: string[];
}

export class GroupAddVM extends UpdateVM{
    public name: string;
    public description: string; 
    public isMain: boolean;
    public isPrivate: boolean;
    public users: string[];
}