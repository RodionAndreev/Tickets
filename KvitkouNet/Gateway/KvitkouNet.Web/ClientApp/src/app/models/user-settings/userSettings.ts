import { VisibleSettings } from './visible';
import { NotificationSettings } from './notifications';

export class UserSettings {
    isGetTicketInfo: boolean = false;
    isPrivateAccount: boolean = false;
    notifications: NotificationSettings;
    preferAddress: string;
    preferRegion: string;
    preferPlace: string;
    settingsId: string;
    userImage?: null;   
    visibleInfo: VisibleSettings;
}
