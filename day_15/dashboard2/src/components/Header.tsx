import React from 'react';

const Header = () => {
  return (
    <header className="bg-white border-b border-gray-200">
      <div className="flex flex-wrap items-center justify-between px-4 py-3">
        <div className="flex items-center">
          <h1 className="text-xl font-semibold text-gray-900 mr-4">Team List</h1>
          <nav className="text-sm">
            <ul className="flex">
              <li className="text-blue-600 hover:underline"><a href="#">Admin Dashboard</a></li>
              <li className="before:content-['/'] before:mx-2 before:text-gray-400">Team List</li>
            </ul>
          </nav>
        </div>
        <div className="flex items-center space-x-4 mt-2 sm:mt-0">
          <div className="relative">
            <input
              type="text"
              placeholder="Search Task..."
              className="py-2 pl-10 pr-4 w-48 sm:w-64 bg-gray-100 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
            {/* <Lucide.Search className="absolute left-3 top-2.5 text-gray-400" size={18} /> */}
          </div>
          <button className="relative p-2 text-gray-600 hover:bg-gray-100 rounded-full">
            {/* <Lucide.Mail size={20} /> */}
            <span className="absolute top-0 right-0 h-5 w-5 flex items-center justify-center bg-red-500 text-white text-xs rounded-full">4</span>
          </button>
          <button className="relative p-2 text-gray-600 hover:bg-gray-100 rounded-full">
            {/* <Lucide.Bell size={20} /> */}
            <span className="absolute top-0 right-0 h-5 w-5 flex items-center justify-center bg-red-500 text-white text-xs rounded-full">17</span>
          </button>
          <button className="flex items-center px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700">
            {/* <Lucide.Plus size={18} className="mr-1" /> */}
            Add User
          </button>
        </div>
      </div>
    </header>
  );
};

export default Header;