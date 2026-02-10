import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../../../config/theme/app_theme.dart';
import '../controllers/onboarding_controller.dart';

/// Screen responsible for the multi-step onboarding process.
///
/// This screen manages a [PageView] to display various onboarding steps
/// (Welcome, Features, Setup). It synchronizes with the [OnboardingController]
/// to persist the completion state.
class OnboardingScreen extends ConsumerStatefulWidget {
  const OnboardingScreen({super.key});

  @override
  ConsumerState<OnboardingScreen> createState() => _OnboardingScreenState();
}

class _OnboardingScreenState extends ConsumerState<OnboardingScreen> {
  final PageController _pageController = PageController();
  int _currentPage = 0;

  // Define onboarding pages data
  final List<_OnboardingPageData> _pages = [
    _OnboardingPageData(
      title: 'Track Your Reading',
      description:
          'Keep a digital log of every book you read. Monitor your progress and build a lasting reading habit.',
      icon: Icons.auto_stories,
    ),
    _OnboardingPageData(
      title: 'Set Goals',
      description:
          'Challenge yourself with daily, weekly, or yearly reading goals. Stay motivated and achieve more.',
      icon: Icons.flag_rounded,
    ),
    _OnboardingPageData(
      title: 'Insightful Stats',
      description:
          'Visualize your reading trends. Understand your pace, favorite genres, and reading frequency.',
      icon: Icons.insights_rounded,
    ),
  ];

  @override
  void dispose() {
    _pageController.dispose();
    super.dispose();
  }

  /// Advances to the next page or completes onboarding if on the last page.
  Future<void> _handleNext() async {
    if (_currentPage < _pages.length - 1) {
      _pageController.nextPage(
        duration: const Duration(milliseconds: 300),
        curve: Curves.easeInOut,
      );
    } else {
      await _completeOnboarding();
    }
  }

  /// Calls the controller to mark onboarding as complete.
  Future<void> _completeOnboarding() async {
    try {
      await ref.read(onboardingControllerProvider.notifier).completeOnboarding();
      // Navigation is handled by AppRouter listening to the user state change
    } catch (e) {
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text('Error completing setup: $e')),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final colorScheme = theme.colorScheme;
    final isLastPage = _currentPage == _pages.length - 1;

    return Scaffold(
      backgroundColor: colorScheme.background,
      body: SafeArea(
        child: Column(
          children: [
            // Skip Button
            Align(
              alignment: Alignment.topRight,
              child: Padding(
                padding: const EdgeInsets.only(right: 16.0, top: 8.0),
                child: TextButton(
                  onPressed: _completeOnboarding,
                  child: Text(
                    isLastPage ? '' : 'Skip',
                    style: TextStyle(color: colorScheme.primary),
                  ),
                ),
              ),
            ),

            // Page View Content
            Expanded(
              child: PageView.builder(
                controller: _pageController,
                onPageChanged: (index) {
                  setState(() {
                    _currentPage = index;
                  });
                },
                itemCount: _pages.length,
                itemBuilder: (context, index) {
                  final page = _pages[index];
                  return _OnboardingContent(
                    title: page.title,
                    description: page.description,
                    icon: page.icon,
                  );
                },
              ),
            ),

            // Bottom Controls (Indicators and Buttons)
            Padding(
              padding: const EdgeInsets.all(24.0),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  // Page Indicators
                  Row(
                    children: List.generate(
                      _pages.length,
                      (index) => AnimatedContainer(
                        duration: const Duration(milliseconds: 300),
                        margin: const EdgeInsets.only(right: 8),
                        height: 8,
                        width: _currentPage == index ? 24 : 8,
                        decoration: BoxDecoration(
                          color: _currentPage == index
                              ? colorScheme.primary
                              : colorScheme.surfaceVariant,
                          borderRadius: BorderRadius.circular(4),
                        ),
                      ),
                    ),
                  ),

                  // Next / Get Started Button
                  ElevatedButton(
                    onPressed: _handleNext,
                    style: ElevatedButton.styleFrom(
                      backgroundColor: colorScheme.primary,
                      foregroundColor: colorScheme.onPrimary,
                      padding: const EdgeInsets.symmetric(
                        horizontal: 24,
                        vertical: 12,
                      ),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(30),
                      ),
                    ),
                    child: Row(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        Text(isLastPage ? 'Get Started' : 'Next'),
                        if (!isLastPage) ...[
                          const SizedBox(width: 8),
                          const Icon(Icons.arrow_forward, size: 18),
                        ],
                      ],
                    ),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}

/// Helper widget to render individual page content.
class _OnboardingContent extends StatelessWidget {
  final String title;
  final String description;
  final IconData icon;

  const _OnboardingContent({
    required this.title,
    required this.description,
    required this.icon,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    
    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 24.0),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Container(
            padding: const EdgeInsets.all(32),
            decoration: BoxDecoration(
              color: theme.colorScheme.primaryContainer.withOpacity(0.3),
              shape: BoxShape.circle,
            ),
            child: Icon(
              icon,
              size: 80,
              color: theme.colorScheme.primary,
            ),
          ),
          const SizedBox(height: 48),
          Text(
            title,
            style: theme.textTheme.headlineMedium?.copyWith(
              fontWeight: FontWeight.bold,
              color: theme.colorScheme.onBackground,
            ),
            textAlign: TextAlign.center,
          ),
          const SizedBox(height: 16),
          Text(
            description,
            style: theme.textTheme.bodyLarge?.copyWith(
              color: theme.colorScheme.onBackground.withOpacity(0.7),
              height: 1.5,
            ),
            textAlign: TextAlign.center,
          ),
        ],
      ),
    );
  }
}

/// Data holder for onboarding pages.
class _OnboardingPageData {
  final String title;
  final String description;
  final IconData icon;

  _OnboardingPageData({
    required this.title,
    required this.description,
    required this.icon,
  });
}