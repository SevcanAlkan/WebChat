import { BaseVM, AddVM, UpdateVM } from '@app/common/baseModel';

export class GroupVM extends BaseVM{
    public name: string;
    public description: string; 
    public isMain: boolean;
    public isPrivate: boolean;
    public users: string[];
}

export class GroupAddVM extends AddVM{
    public name: string;
    public description: string; 
    public isMain: boolean;
    public isPrivate: boolean;
    public users: string[];
}

export class GroupUpdateVM extends UpdateVM{
    public name: string;
    public description: string; 
    public isMain: boolean;
    public isPrivate: boolean;
    public users: string[];
}