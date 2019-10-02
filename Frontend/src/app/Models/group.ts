import { BaseVM } from '@app/common/baseModel';

export class Group extends BaseVM{
    public name: string;
    public description: string; 
    public isMain: boolean;
    public isPrivate: boolean;
    public users: string[];
}