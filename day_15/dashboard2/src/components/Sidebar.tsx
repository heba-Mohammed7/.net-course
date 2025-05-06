import React from 'react';
import { List, ListItem, ListItemIcon } from '@mui/material';
import DashboardIcon from '@mui/icons-material/Dashboard';
import PeopleIcon from '@mui/icons-material/People';
import FolderIcon from '@mui/icons-material/Folder';
import EventIcon from '@mui/icons-material/Event';
import SettingsIcon from '@mui/icons-material/Settings';

const Sidebar: React.FC = () => {
  return (
    <List sx={{ padding: 0, flexShrink: 0 }}>
      <ListItem sx={{ padding: '8px', justifyContent: 'center' }}>
        <ListItemIcon sx={{ minWidth: 0, color: '#007bff' }}>
          <DashboardIcon />
        </ListItemIcon>
      </ListItem>
      <ListItem sx={{ padding: '8px', justifyContent: 'center' }}>
        <ListItemIcon sx={{ minWidth: 0 }}>
          <PeopleIcon />
        </ListItemIcon>
      </ListItem>
      <ListItem sx={{ padding: '8px', justifyContent: 'center' }}>
        <ListItemIcon sx={{ minWidth: 0 }}>
          <FolderIcon />
        </ListItemIcon>
      </ListItem>
      <ListItem sx={{ padding: '8px', justifyContent: 'center' }}>
        <ListItemIcon sx={{ minWidth: 0 }}>
          <EventIcon />
        </ListItemIcon>
      </ListItem>
      <ListItem sx={{ padding: '8px', justifyContent: 'center' }}>
        <ListItemIcon sx={{ minWidth: 0 }}>
          <SettingsIcon />
        </ListItemIcon>
      </ListItem>
    </List>
  );
};

export default Sidebar;