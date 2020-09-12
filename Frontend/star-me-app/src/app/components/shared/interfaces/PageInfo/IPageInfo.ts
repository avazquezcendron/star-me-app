export interface IPageInfo {
  title: string;
  navBar: INavBar;
  footer: IFooter;
  index: IIndex;
  header: IHeader;
  services: IServices;
  contact: IContact;
}

export interface IFooter {
  email: string;
  copyright: string;
}

export interface IIndex {}

export interface INavBar {
  navBarTitle: string;
  menu: IMenu;
}

export interface IMenu {
  subMenu1: string;
  subMenu2: string;
  subMenu3: string;
  subMenu4: string;
  contactUs: string;
}

export interface IHeader {
  title: string;
  description: string;
}

export interface IServices {
  title: string;
  description: string;
  services: IService[];
}

export interface IService {
  title: string;
  description: IServiceItem[];
  icon: string;
}

export interface IContact {
  title: string;
  description: string;
  labelBtnEnviar: string;
  team: ITeam[];
}

export interface ITeam {
  name: string;
  role: string;
  img: string;
}

export interface IServiceItem {
  item: string;
}
