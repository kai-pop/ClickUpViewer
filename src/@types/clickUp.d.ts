export interface Space {
  id: number;
  name: string;
}

export interface Folder {
  id: number;
  name: string;
}

export interface List {
  id: number;
  name: string;
}

export interface Task {
  id: string;
  name: string;
  status: {
    status: string;
    color: string;
    orderindex: number;
    type: string;
  };
  orderindex: number;
  date_created: number;
  date_updated: number;
  date_closed: number;
  creator: UserClickUp;
  assignees: number[];
  checklists: any[];
  tags: string[];
  parent: number;
  due_date: number;
  start_date: number;
  time_estimate: number;
  time_spent: number;
  list: {
    id: number;
  };
  folder: {
    id: number;
  };
  space: {
    id: number;
  };
  url: string;
}

export interface UserClickUp {
  id: number;
  username: string;
  email: string;
  color?: string;
  profilePicture: string;
  initials: string;
}

export interface GetTaskParameter {
  archived: boolean;
  page: number
  subtasks: boolean;
  statuses: string[];
  include_closed: boolean;
}