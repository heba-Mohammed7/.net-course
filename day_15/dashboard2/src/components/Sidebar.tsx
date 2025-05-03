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
      <ListItem sx={{ padding: { xs: '8px 4px', sm: '8px 12px' }, justifyContent: 'center' }}>
        <ListItemIcon sx={{ minWidth: 0 }}>
          <DashboardIcon fontSize="small" />
        </ListItemIcon>
      </ListItem>
      <ListItem sx={{ padding: { xs: '8px 4px', sm: '8px 12px' }, justifyContent: 'center' }}>
        <ListItemIcon sx={{ minWidth: 0 }}>
          <PeopleIcon fontSize="small" />
        </ListItemIcon>
      </ListItem>
      <ListItem sx={{ padding: { xs: '8px 4px', sm: '8px 12px' }, justifyContent: 'center' }}>
        <ListItemIcon sx={{ minWidth: 0 }}>
          <FolderIcon fontSize="small" />
        </ListItemIcon>
      </ListItem>
      <ListItem sx={{ padding: { xs: '8px 4px', sm: '8px 12px' }, justifyContent: 'center' }}>
        <ListItemIcon sx={{ minWidth: 0 }}>
          <EventIcon fontSize="small" />
        </ListItemIcon>
      </ListItem>
      <ListItem sx={{ padding: { xs: '8px 4px', sm: '8px 12px' }, justifyContent: 'center' }}>
        <ListItemIcon sx={{ minWidth: 0 }}>
          <SettingsIcon fontSize="small" />
        </ListItemIcon>
      </ListItem>
    </List>
  );
};

export default Sidebar;