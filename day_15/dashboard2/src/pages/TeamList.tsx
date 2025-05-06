import React, { useState } from 'react';
import { Box, Typography, AppBar, Toolbar, InputBase, Button, IconButton, Badge, Breadcrumbs, Link, useMediaQuery, Skeleton } from '@mui/material';
import { styled, alpha, useTheme } from '@mui/material/styles';
import SearchIcon from '@mui/icons-material/Search';
import NotificationsIcon from '@mui/icons-material/Notifications';
import MailIcon from '@mui/icons-material/Mail';
import MenuIcon from '@mui/icons-material/Menu';
import Drawer from '@mui/material/Drawer';
import Sidebar from '../components/Sidebar';
import UserTable from '../components/UserTable';

const Search = styled('div')(({ theme }) => ({
  position: 'relative',
  borderRadius: theme.shape.borderRadius,
  backgroundColor: alpha(theme.palette.grey[200], 0.5),
  '&:hover': {
    backgroundColor: alpha(theme.palette.grey[200], 0.7),
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
    [theme.breakpoints.down('sm')]: {
      width: '10ch',
      '&:focus': {
        width: '15ch',
      },
    },
  },
}));

const TeamList: React.FC = () => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(true); // Simulated loading state

  // Simulate loading for header and sidebar
  React.useEffect(() => {
    const timer = setTimeout(() => setIsLoading(false), 1000); // Simulate 1s load
    return () => clearTimeout(timer);
  }, []);

  const toggleSidebar = () => {
    setSidebarOpen(!sidebarOpen);
  };

  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh', backgroundColor: '#e9ecef' }}>
      <AppBar
        position="fixed"
        sx={{
          backgroundColor: '#fff',
          color: '#000',
          boxShadow: 'none',
          borderBottom: '1px solid #dee2e6',
          zIndex: theme.zIndex.drawer + 1,
        }}
      >
        <Toolbar
          sx={{
            flexWrap: 'nowrap',
            justifyContent: 'space-between',
            p: { xs: 0.5, sm: 1 },
            minHeight: { xs: 48, sm: 64 },
          }}
        >
          {isLoading ? (
            <Box sx={{ display: 'flex', alignItems: 'center', width: '100%', gap: 1 }}>
              <Skeleton variant="circular" width={24} height={24} />
              <Skeleton variant="text" width={100} />
              <Skeleton variant="rectangular" width={80} height={20} sx={{ ml: 'auto' }} />
            </Box>
          ) : (
            <>
              <Box sx={{ display: 'flex', alignItems: 'center', flexShrink: 0 }}>
                {isMobile && (
                  <IconButton
                    edge="start"
                    color="inherit"
                    aria-label="menu"
                    onClick={toggleSidebar}
                    sx={{ mr: 0.5 }}
                  >
                    <MenuIcon fontSize="small" />
                  </IconButton>
                )}
                <Typography
                  variant="h6"
                  component="div"
                  sx={{ fontSize: { xs: '1rem', sm: '1.25rem' }, mr: 1 }}
                >
                  Team List
                </Typography>
                {!isMobile && (
                  <Breadcrumbs aria-label="breadcrumb" sx={{ fontSize: '0.75rem' }}>
                    <Link underline="hover" color="inherit" href="/admin">
                      Admin Dashboard
                    </Link>
                    <Typography color="text.primary">Team List</Typography>
                  </Breadcrumbs>
                )}
              </Box>
              <Box
                sx={{
                  display: 'flex',
                  alignItems: 'center',
                  flexShrink: 0,
                  gap: { xs: 0.5, sm: 1 },
                }}
              >
                <Search
                  sx={{
                    minWidth: 'auto',
                    mr: { xs: 0, sm: 1 },
                    backgroundColor: '#f1f3f5',
                  }}
                >
                  <SearchIconWrapper>
                    <SearchIcon fontSize="small" />
                  </SearchIconWrapper>
                  <StyledInputBase
                    placeholder="Search..."
                    inputProps={{ 'aria-label': 'search' }}
                    sx={{ fontSize: { xs: '0.75rem', sm: '0.875rem' } }}
                  />
                </Search>
                <IconButton
                  size="small"
                  aria-label="show 4 new mails"
                  color="inherit"
                  sx={{ p: { xs: 0.5, sm: 1 } }}
                >
                  <Badge badgeContent={4} color="error">
                    <MailIcon fontSize="small" />
                  </Badge>
                </IconButton>
                <IconButton
                  size="small"
                  aria-label="show 17 new notifications"
                  color="inherit"
                  sx={{ p: { xs: 0.5, sm: 1 } }}
                >
                  <Badge badgeContent={17} color="error">
                    <NotificationsIcon fontSize="small" />
                  </Badge>
                </IconButton>
                <Button
                  variant="contained"
                  color="primary"
                  sx={{
                    ml: { xs: 0, sm: 1 },
                    p: { xs: '4px 8px', sm: '6px 16px' },
                    fontSize: { xs: '0.75rem', sm: '0.875rem' },
                    minWidth: 'auto',
                  }}
                >
                  + Add
                </Button>
              </Box>
            </>
          )}
        </Toolbar>
      </AppBar>
      <Box sx={{ display: 'flex', flex: 1, mt: { xs: '48px', sm: '64px' } }}>
        <Drawer
          variant={isMobile ? 'temporary' : 'permanent'}
          open={isMobile ? sidebarOpen : true}
          onClose={toggleSidebar}
          sx={{
            width: 64,
            flexShrink: 0,
            '& .MuiDrawer-paper': {
              width: 64,
              boxSizing: 'border-box',
              backgroundColor: '#fff',
              top: { xs: '48px', sm: '64px' },
              height: { xs: 'calc(100vh - 48px)', sm: 'calc(100vh - 64px)' },
              borderRight: '1px solid #dee2e6',
              display: 'flex',
              flexDirection: 'column',
              justifyContent: 'flex-start',
              boxShadow: '2px 0 5px rgba(0,0,0,0.1)',
            },
          }}
        >
          {isLoading ? (
            <Box sx={{ p: 1 }}>
              {Array(5)
                .fill(0)
                .map((_, index) => (
                  <Skeleton key={index} variant="circular" width={24} height={24} sx={{ mb: 1, mx: 'auto' }} />
                ))}
            </Box>
          ) : (
            <Sidebar />
          )}
        </Drawer>
        <Box
          component="main"
          sx={{
            flexGrow: 1,
            p: { xs: 1, sm: 2 },
            backgroundColor: '#e9ecef',
          }}
        >
          <Box
            sx={{
              p: 2,
              backgroundColor: '#fff',
              borderRadius: 8,
              boxShadow: '0 2px 4px rgba(0,0,0,0.1)',
            }}
          >
            {isLoading ? (
              <Skeleton variant="text" width={100} />
            ) : (
              <Typography
                variant="body2"
                sx={{ mb: 1, color: '#868e96', fontSize: { xs: '0.75rem', sm: '0.875rem' } }}
              >
                2 Selected
              </Typography>
            )}
            <UserTable />
          </Box>
        </Box>
      </Box>
    </Box>
  );
};

export default TeamList;