import React from 'react';
import { Box, Typography, AppBar, Toolbar, InputBase, Button, IconButton, Badge } from '@mui/material';
import { styled, alpha } from '@mui/material/styles';
import SearchIcon from '@mui/icons-material/Search';
import NotificationsIcon from '@mui/icons-material/Notifications';
import MailIcon from '@mui/icons-material/Mail';
import Drawer from '@mui/material/Drawer';
import Sidebar from '../components/Sidebar';
import UserTable from '../components/UserTable';

const Search = styled('div')(({ theme }) => ({
  position: 'relative',
  borderRadius: theme.shape.borderRadius,
  backgroundColor: alpha(theme.palette.common.white, 0.15),
  '&:hover': {
    backgroundColor: alpha(theme.palette.common.white, 0.25),
  },
  marginLeft: 0,
  width: '100%',
  [theme.breakpoints.up('sm')]: {
    marginLeft: theme.spacing(1),
    width: 'auto',
  },
}));

const SearchIconWrapper = styled('div')(({ theme }) => ({
  padding: theme.spacing(0, 2),
  height: '100%',
  position: 'absolute',
  pointerEvents: 'none',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
}));

const StyledInputBase = styled(InputBase)(({ theme }) => ({
  color: 'inherit',
  '& .MuiInputBase-input': {
    padding: theme.spacing(1, 1, 1, 0),
    paddingLeft: `calc(1em + ${theme.spacing(4)})`,
    transition: theme.transitions.create('width'),
    width: '100%',
    [theme.breakpoints.up('sm')]: {
      width: '12ch',
      '&:focus': {
        width: '20ch',
      },
    },
  },
}));

const TeamList: React.FC = () => {
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static" sx={{ backgroundColor: '#fff', color: '#000', boxShadow: 'none', borderBottom: '1px solid #e0e0e0' }}>
        <Toolbar sx={{ flexWrap: 'wrap', justifyContent: 'space-between' }}>
          <Box sx={{ display: 'flex', alignItems: 'center', flexWrap: 'wrap', mb: { xs: 1, sm: 0 } }}>
            <Typography variant="h6" component="div" sx={{ mr: 2 }}>
              Team List
            </Typography>

          </Box>
          <Box sx={{ display: 'flex', alignItems: 'center', flexWrap: 'wrap' }}>
            <Search sx={{ minWidth: { xs: '100%', sm: 'auto' }, mb: { xs: 1, sm: 0 }, mr: { sm: 2 } }}>
              <SearchIconWrapper>
                <SearchIcon />
              </SearchIconWrapper>
              <StyledInputBase placeholder="Search Task..." inputProps={{ 'aria-label': 'search' }} />
            </Search>
            <IconButton size="large" aria-label="show 4 new mails" color="inherit">
              <Badge badgeContent={4} color="error">
                <MailIcon />
              </Badge>
            </IconButton>
            <IconButton size="large" aria-label="show 17 new notifications" color="inherit">
              <Badge badgeContent={17} color="error">
                <NotificationsIcon />
              </Badge>
            </IconButton>
            <Button variant="contained" sx={{ ml: { sm: 2 }, mt: { xs: 1, sm: 0 } }}>Add User</Button>
          </Box>
        </Toolbar>
      </AppBar>
      <Box sx={{ display: 'flex' }}>
        <Drawer
          variant="permanent"
          sx={{
            width: { xs: 48, sm: 60 },
            flexShrink: 0,
            '& .MuiDrawer-paper': {
              width: { xs: 48, sm: 60 },
              boxSizing: 'border-box',
              backgroundColor: '#f5f7fa',
              overflowY: 'hidden',
              height: '100vh',
              display: 'flex',
              flexDirection: 'column',
              justifyContent: 'flex-start',
            },
          }}
        >
          <Sidebar />
        </Drawer>
        <Box component="main" sx={{ flexGrow: 1, p: { xs: 1, sm: 3 }, backgroundColor: '#f0f2f5' }}>
          <UserTable />
        </Box>
      </Box>
    </Box>
  );
};

export default TeamList;